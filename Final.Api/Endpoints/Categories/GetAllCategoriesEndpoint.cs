using Final.Api.Common.API;
using Final.Core.Models;
using Final.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Final.Core;
using Final.Core.Requests.Categories;
using Fina.Core.Response;

namespace Final.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", ServiceAsync)
        .WithName("Categories: Get All")
        .WithSummary("Obtém todas as categorias")
        .WithDescription("Obtém todas as categorias")
        .WithOrder(5)
        .Produces<Response<List<Category>?>>();

    private static async Task<IResult> ServiceAsync(ICategoryService service, 
        [FromQuery] int pageNumber = Configurations.DefaultPageNumber,
        [FromQuery] int pageSize = Configurations.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = ApiConfiguration.UserId,
            PageNumber = pageNumber,
            PageSize = pageSize
        }; 
        
        var response  =  await service.GetAllAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}

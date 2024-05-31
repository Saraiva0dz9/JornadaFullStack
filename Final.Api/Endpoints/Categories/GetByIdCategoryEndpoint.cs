using Final.Api.Common.API;
using Final.Core.Services;
using Final.Core.Requests.Categories;
using Final.Core.Models;
using Final.Core.Responses;

namespace Final.Api.Endpoints.Categories;

public class GetByIdCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", ServiceAsync)
        .WithName("Categories: Get By Id")
        .WithSummary("Obtém uma categoria pelo Id")
        .WithDescription("Obtém uma categoria pelo Id")
        .WithOrder(4)
        .Produces<Response<Category?>>();
    
    public static async Task<IResult> ServiceAsync(ICategoryService service, long id)
    {
        var request = new GetCategoryByIdRequest
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var response = await service.GetByIdAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}

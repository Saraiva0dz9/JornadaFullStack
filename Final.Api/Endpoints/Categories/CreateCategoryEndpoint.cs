using Final.Api.Common.API;
using Final.Core.Models;
using Final.Core.Requests.Categories;
using Final.Core.Services;
using Final.Core.Responses;

namespace Final.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", ServiceAsync)
        .WithName("Categories: Create")
        .WithSummary("Cria uma nova categoria")
        .WithDescription("Cria uma nova categoria")
        .WithOrder(1)
        .Produces<Response<Category?>>();

    private static async Task<IResult> ServiceAsync(ICategoryService service, CreateCategoryRequest request)
    {
        request.UserId = ApiConfiguration.UserId;
        var response = await service.CreateAsync(request);
        return response.IsSuccess 
            ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response)
            : TypedResults.BadRequest(response);
    }
}

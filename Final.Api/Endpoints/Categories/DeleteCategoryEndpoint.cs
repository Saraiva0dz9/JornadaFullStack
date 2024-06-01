using Final.Api.Common.API;
using Final.Core.Services;
using Final.Core.Models;
using Final.Core.Requests.Categories;
using Fina.Core.Response;

namespace Final.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", ServiceAsync)
        .WithName("Categories: Delete")
        .WithSummary("Deleta uma categoria")
        .WithDescription("Deleta uma categoria")
        .WithOrder(3)
        .Produces<Response<Category?>>();

    public static async Task<IResult> ServiceAsync(ICategoryService service, long id)
    {
        var request = new DeleteCategoryRequest
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var response = await service.DeleteAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}

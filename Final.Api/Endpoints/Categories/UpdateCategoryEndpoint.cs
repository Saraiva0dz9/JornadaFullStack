using Fina.Core.Response;
using Final.Api.Common.API;
using Final.Core.Models;
using Final.Core.Requests.Categories;
using Final.Core.Services;

namespace Final.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", ServiceAsync)
        .WithName("Categories: Update")
        .WithSummary("Atualiza uma categoria")
        .WithDescription("Atualiza uma categoria")
        .WithOrder(2)
        .Produces<Response<Category?>>();

    public static async Task<IResult> ServiceAsync(ICategoryService service, long id, UpdateCategoryRequest request)
    {
        request.UserId = ApiConfiguration.UserId;
        request.Id = id;

        var response = await service.UpdateAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }

}

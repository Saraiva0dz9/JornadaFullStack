using Final.Api.Common.API;
using Final.Api.Endpoints.Categories;

namespace Final.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Healt Check")
            .MapGet("/", () => new { message = "OK" });

        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>() 
            .MapEndpoint<GetAllCategoriesEndpoint>()
            .MapEndpoint<GetByIdCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}

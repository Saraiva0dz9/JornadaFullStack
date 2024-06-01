using Final.Api.Common.API;
using Final.Api.Endpoints.Categories;
using Final.Api.Endpoints.Transactions;

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
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>() 
            .MapEndpoint<GetAllCategoriesEndpoint>()
            .MapEndpoint<GetByIdCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>();

        endpoints.MapGroup("v1/transactions")
            .WithTags("Transactions")
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<DeleteTransactionEndpoint>()
            .MapEndpoint<GetByPeriodEndpoint>()
            .MapEndpoint<GetByIdTransactionEndpoint>()
            .MapEndpoint<UpdateTransactionEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}

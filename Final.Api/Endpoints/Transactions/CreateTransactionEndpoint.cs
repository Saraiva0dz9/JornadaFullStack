using Fina.Core.Response;
using Final.Api.Common.API;
using Final.Core.Models;
using Final.Core.Requests.Transactions;
using Final.Core.Responses;
using Final.Core.Services;

namespace Final.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", ServiceAsync)
        .WithName("Transactions: Create")
        .WithSummary("Cria uma nova transação")
        .WithDescription("Cria uma nova transação")
        .WithOrder(1)
        .Produces<Response<Transaction?>>();

    private static async Task<IResult> ServiceAsync(ITransactionService service, CreateTransactionRequest request)
    {
        request.UserId = ApiConfiguration.UserId;

        var response = await service.CreateAsync(request);
        return response.IsSuccess
            ? TypedResults.Created($"/{response.Data?.Id}", response)
            : TypedResults.BadRequest(response);
    }
}
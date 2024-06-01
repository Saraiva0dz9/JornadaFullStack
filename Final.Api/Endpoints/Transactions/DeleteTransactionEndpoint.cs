using Fina.Core.Response;
using Final.Api.Common.API;
using Final.Core.Models;
using Final.Core.Requests.Transactions;
using Final.Core.Services;

namespace Final.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", ServiceAsync)
        .WithName("Transactions: Delete")
        .WithSummary("Deleta uma transação")
        .WithDescription("Deleta uma transação")
        .WithOrder(3)
        .Produces<Response<Transaction?>>();

    public static async Task<IResult> ServiceAsync(ITransactionService service, long id)
    {
        var request = new DeleteTransactionRequest
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

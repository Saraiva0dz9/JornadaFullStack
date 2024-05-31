using Final.Api.Common.API;
using Final.Core.Models;
using Final.Core.Requests.Transactions;
using Final.Core.Responses;
using Final.Core.Services;

namespace Final.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", ServiceAsync)
        .WithName("Transactions: Update")
        .WithSummary("Atualiza uma transação")
        .WithDescription("Atualiza uma transação")
        .WithOrder(2)
        .Produces<Response<Transaction?>>();

    private static async Task<IResult> ServiceAsync(ITransactionService service, UpdateTransactionRequest request, long id)
    {
        request.UserId = ApiConfiguration.UserId;
        request.Id = id;

        var response = await service.UpdateAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}

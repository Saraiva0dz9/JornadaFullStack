using Final.Api.Common.API;
using Final.Core.Models;
using Final.Core.Responses;
using Final.Core.Services;
using Final.Core.Requests.Transactions;

namespace Final.Api.Endpoints.Transactions;

public class GetByIdTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", ServiceAsync)
        .WithName("Transactions: GetById")
        .WithSummary("Obtém uma transação pelo ID")
        .WithDescription("Obtém uma transação pelo ID")
        .WithOrder(4)
        .Produces<Response<Transaction?>>();

    private static async Task<IResult> ServiceAsync(ITransactionService service, long id)
    {
        var request = new GetTransactionByIdRequest
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

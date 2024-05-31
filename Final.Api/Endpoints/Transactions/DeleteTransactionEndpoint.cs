using Final.Api.Common.API;
using Final.Core.Requests.Transactions;
using Final.Core.Services;

namespace Final.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        throw new NotImplementedException();
    }

    public static async Task<IResult> ServiceAsync(ITransactionService service, DeleteTransactionRequest request, long id)
    {
        request.UserId = ApiConfiguration.UserId;
    }
}

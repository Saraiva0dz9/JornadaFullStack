using Fina.Core.Response;
using Final.Api.Common.API;
using Final.Core;
using Final.Core.Models;
using Final.Core.Requests.Transactions;
using Final.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Final.Api.Endpoints.Transactions;

public class GetByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", ServiceAsync)
        .WithName("Transactions: GetByPeriod")
        .WithSummary("Obtém transações por período")
        .WithDescription("Obtém transações por período")
        .WithOrder(5)
        .Produces<Response<List<Transaction>>>();

    private static async Task<IResult> ServiceAsync(ITransactionService service,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = Configurations.DefaultPageNumber,
        [FromQuery] int pageSize = Configurations.DefaultPageSize)
    {
        var request = new GetTransactionByPeriodRequest
        {
            UserId = ApiConfiguration.UserId,
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = pageNumber,
            PageSize = pageSize
        };         

        var response = await service.GetByPeriodAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}

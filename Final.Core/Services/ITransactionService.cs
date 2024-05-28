using Final.Core.Requests.Transactions;
using Final.Core.Responses;
using System.Transactions;

namespace Final.Core.Services;

public interface ITransactionService
{
    Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
    Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
    Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
    Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
    Task<PagedResponse<Transaction?>> GetByPeriodAsync(GetTransactionByPeriodRequest request);
}

using Final.Core.Enums;
using Final.Core.Requests.Transactions;
using Final.Core.Responses;
using Final.Core.Services;
using Final.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Final.Api.Data.Services
{
    public class TransactionService(AppDbContext context, ILogger<Transaction> logger) : ITransactionService
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: ETransactionType.Withdraw, Amount: > 0 })
                request.Amount *= -1;
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "TransactionService.CreateAsync");
                throw;
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação excluída com sucesso!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "TransactionService.DeleteAsync");
                throw;
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<Transaction?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: ETransactionType.Withdraw, Amount: > 0 })
                request.Amount *= -1;
            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "TransactionService.UpdateAsync");
                throw;
            }
        }
    }
}

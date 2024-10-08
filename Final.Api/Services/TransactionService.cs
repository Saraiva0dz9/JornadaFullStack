﻿using Final.Core.Enums;
using Final.Core.Requests.Transactions;
using Final.Core.Responses;
using Final.Core.Services;
using Final.Core.Models;
using Microsoft.EntityFrameworkCore;
using Final.Core.Common;
using Final.Api.Data;
using Fina.Core.Response;

namespace Final.Api.Services
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
            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transaction is null
                    ? new Response<Transaction?>(null, 404, "Transação não encontrada")
                    : new Response<Transaction?>(transaction);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "TransactionService.GetByIdAsync");
                throw;
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível determinar a data de início ou término");
            }

            try
            {
                var query = context
                    .Transactions
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId && x.PaidOrReceivedAt >= request.StartDate && x.PaidOrReceivedAt <= request.EndDate)
                    .OrderBy(x => x.PaidOrReceivedAt);

                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "TransactionService.GetByPeriodAsync");
                throw;
            }
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

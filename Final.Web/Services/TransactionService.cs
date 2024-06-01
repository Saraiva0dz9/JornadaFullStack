using Fina.Core.Response;
using Final.Core.Models;
using Final.Core.Requests.Transactions;
using Final.Core.Responses;
using Final.Core.Services;
using System.Net.Http.Json;

namespace Final.Web.Services;

public class TransactionService(IHttpClientFactory httpClientFactory, ILogger<TransactionService> logger) : ITransactionService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);

    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("v1/transactions", request);

            return await response.Content.ReadFromJsonAsync<Response<Transaction?>>()
                ?? new Response<Transaction?>(null, 400, "Falha ao criar transação");
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
            var response = await _httpClient.DeleteAsync($"v1/transactions/{request.Id}");

            return await response.Content.ReadFromJsonAsync<Response<Transaction?>>()
                ?? new Response<Transaction?>(null, 400, "Falha ao excluir transação");
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
            var response = await _httpClient.GetAsync($"v1/transactions/{request.Id}");

            return await response.Content.ReadFromJsonAsync<Response<Transaction?>>()
                ?? new Response<Transaction?>(null, 400, "Falha ao obter transação");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "TransactionService.GetByIdAsync");
            throw;
        }        
    }

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"v1/transactions/{request.Id}", request);

            return await response.Content.ReadFromJsonAsync<Response<Transaction?>>()
                ?? new Response<Transaction?>(null, 400, "Falha ao atualizar transação");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "TransactionService.UpdateAsync");
            throw;
        }
    }
}

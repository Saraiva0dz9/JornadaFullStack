using Fina.Core.Response;
using Final.Core.Models;
using Final.Core.Requests.Categories;
using Final.Core.Responses;
using Final.Core.Services;
using System.Net.Http.Json;

namespace Final.Web.Services;

public class CategoryService(IHttpClientFactory httpClientFactory, ILogger<CategoryService> logger) : ICategoryService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);

    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("v1/categories", request);

            return await response.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha ao criar categoria");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "CategoryService.CreateAsync");
            throw;
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"v1/categories/{request.Id}");

            return await response.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha ao excluir categoria"); 
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "CategoryService.DeleteAsync");
            throw;
        }
    }

    public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var response = await _httpClient.GetAsync("v1/categories");

            return await response.Content.ReadFromJsonAsync<PagedResponse<List<Category>?>>()
                ?? new PagedResponse<List<Category>?>(null, 400, "Falha ao obter categorias");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "CategoryService.GetAllAsync");
            throw;
        }        
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var response = await _httpClient.GetAsync($"v1/categories/{request.Id}");

            return await response.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha ao obter categoria");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "CategoryService.GetByIdAsync");
            throw;
        }        
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"v1/categories/{request.Id}", request);

            return await response.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha ao atualizar categoria");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "CategoryService.UpdateAsync");
            throw;
        }
    }
}

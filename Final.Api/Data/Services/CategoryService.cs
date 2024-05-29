using Final.Core.Models;
using Final.Core.Requests.Categories;
using Final.Core.Responses;
using Final.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Final.Api.Data.Services;

public class CategoryService(AppDbContext context, ILogger<CategoryService> logger) : ICategoryService
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                Title = request.Title!,
                Description = request.Description,
                UserId = request.UserId
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201, "Categoria criada com sucesso!");
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
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 200, "Categoria excluída com sucesso!");
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
            var query = context
            .Categories
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .OrderBy(x => x.Title);

            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Category>?>(categories, count, request.PageNumber, request.PageSize);
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
            var category = await context
                .Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return category is null
                ? new Response<Category?>(null, 404, "Categoria não encontrada")
                : new Response<Category?>(category);
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
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");

            category.Title = request.Title!;
            category.Description = request.Description;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 200, "Categoria atualizada com sucesso!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "CategoryService.UpdateAsync");
            throw;
        }
    }
}

using Dima.Api.Data;
using Dima.Core.BaseResponses;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<BaseResponse<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new BaseResponse<Category?>(category, 201);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new BaseResponse<Category?>(null, 500, "Não foi possivel criar a categoria");
            }
        }

        public async Task<BaseResponse<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (category is null)
                {
                    return new BaseResponse<Category?>(null, 404, "Categoria não encontrada");
                }

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new BaseResponse<Category?>(category);
            }
            catch
            {
                return new BaseResponse<Category?>(null, 500, "Não foi possivel deletar a categoria");
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
            try
            {
                var query = context.Categories.AsNoTracking().Where(x => x.UserId == request.UserId).OrderBy(x => x.Title);

                var categories = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
            }
            catch (Exception)
            {
                return new PagedResponse<List<Category>>(null, 500, "Ocorreu um erro ao buscar as categorias");
            }
        }

        public async Task<BaseResponse<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return category is null
                    ? new BaseResponse<Category?>(null, 404, "Nenhuma categoria foi encontrada")
                    : new BaseResponse<Category?>(category);
            }
            catch (Exception)
            {
                return new BaseResponse<Category?>(null, 500, "Não foi possivel obter a categoria");
            }
        }

        public async Task<BaseResponse<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (category is null)
                {
                    return new BaseResponse<Category?>(null, 404, "Categoria não encontrada");
                }

                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);

                await context.SaveChangesAsync();

                return new BaseResponse<Category?>(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new BaseResponse<Category?>(null, 500, "Não foi possivel atualizar a categoria");
            }
        }
    }
}

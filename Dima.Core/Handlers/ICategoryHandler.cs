using Dima.Core.BaseResponses;
using Dima.Core.Model;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface ICategoryHandler
{
    Task<BaseResponse<Category?>> CreateAsync(CreateCategoryRequest request);
    Task<BaseResponse<Category?>> UpdateAsync(UpdateCategoryRequest request);
    Task<BaseResponse<Category?>> DeleteAsync(DeleteCategoryRequest request);
    Task<BaseResponse<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
    Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
}

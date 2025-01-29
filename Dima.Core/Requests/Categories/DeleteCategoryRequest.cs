namespace Dima.Core.Requests.Categories;

public class DeleteCategoryRequest : BaseRequest
{
    public DeleteCategoryRequest(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}

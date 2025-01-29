namespace Dima.Core.Requests.Categories
{
    public class GetCategoryByIdRequest : BaseRequest
    {
        public GetCategoryByIdRequest(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}

namespace Dima.Core.Requests.Transactions
{
    public class GetTransactionByIdRequest : BaseRequest
    {
        public GetTransactionByIdRequest(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}

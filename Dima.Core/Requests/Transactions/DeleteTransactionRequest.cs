namespace Dima.Core.Requests.Transactions
{
    public class DeleteTransactionRequest : BaseRequest
    {
        public DeleteTransactionRequest(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}

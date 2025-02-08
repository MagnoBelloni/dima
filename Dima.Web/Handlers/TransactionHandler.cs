using Dima.Core.BaseResponses;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<BaseResponse<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/transactions", request);
            return await result.Content.ReadFromJsonAsync<BaseResponse<Transaction?>>()
                   ?? new BaseResponse<Transaction?>(null, 400, "Não foi possível criar sua transação");
        }

        public async Task<BaseResponse<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/transactions/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<BaseResponse<Transaction?>>()
                   ?? new BaseResponse<Transaction?>(null, 400, "Não foi possível atualizar sua transação");
        }

        public async Task<BaseResponse<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            var result = await _client.DeleteAsync($"v1/transactions/{request.Id}");
            return await result.Content.ReadFromJsonAsync<BaseResponse<Transaction?>>()
                   ?? new BaseResponse<Transaction?>(null, 400, "Não foi possível excluir sua transação");
        }

        public async Task<BaseResponse<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
            => await _client.GetFromJsonAsync<BaseResponse<Transaction?>>($"v1/transactions/{request.Id}")
               ?? new BaseResponse<Transaction?>(null, 400, "Não foi possível obter a transação");

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            const string format = "yyyy-MM-dd";
            var startDate = request.StartDate is not null
                ? request.StartDate.Value.ToString(format)
                : DateTime.Now.GetFirstDay().ToString(format);

            var endDate = request.EndDate is not null
                ? request.EndDate.Value.ToString(format)
                : DateTime.Now.GetLastDay().ToString(format);

            var url = $"v1/transactions?startDate={startDate}&endDate={endDate}";

            return await _client.GetFromJsonAsync<PagedResponse<List<Transaction>?>>(url)
                ?? new PagedResponse<List<Transaction>?>(null, 400, "Não foi possível obter as transações");
        }
    }
}
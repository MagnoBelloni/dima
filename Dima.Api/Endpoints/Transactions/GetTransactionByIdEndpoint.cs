using Dima.Api.Common.Api;
using Dima.Core.BaseResponses;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Transactions;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync)
               .WithName("Transactions: Get By Id")
               .WithSummary("Obtem uma transação")
               .Produces<BaseResponse<Transaction?>>();
        }

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id, ClaimsPrincipal claimsPrincipal)
        {
            var request = new GetTransactionByIdRequest(id)
            {
                UserId = claimsPrincipal.Identity?.Name ?? string.Empty
            };

            var result = await handler.GetByIdAsync(request);
            if (!result.IsSuccess)
                return TypedResults.BadRequest(result);

            return TypedResults.Ok(result);
        }
    }
}

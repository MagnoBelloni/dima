using Dima.Api.Common.Api;
using Dima.Core.BaseResponses;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Transactions;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", HandleAsync)
               .WithName("Transactions: Update")
               .WithSummary("Atualizar uma transação")
               .Produces<BaseResponse<Transaction?>>();
        }

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler, UpdateTransactionRequest request, long id, ClaimsPrincipal claimsPrincipal)
        {
            request.Id = id;
            request.UserId = claimsPrincipal.Identity?.Name ?? string.Empty;

            var result = await handler.UpdateAsync(request);
            if (!result.IsSuccess)
                return TypedResults.BadRequest(result);

            return TypedResults.Ok(result);
        }
    }
}

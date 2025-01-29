using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Transactions;
using Dima.Core.BaseResponses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("Transactions: Create")
                .WithSummary("Criar uma nova transação")
                .Produces<BaseResponse<Transaction?>>();

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler, CreateTransactionRequest request, ClaimsPrincipal claimsPrincipal)
        {
            request.UserId = claimsPrincipal.Identity?.Name ?? string.Empty;

            var result = await handler.CreateAsync(request);
            if (!result.IsSuccess)
                return TypedResults.BadRequest(result);

            return TypedResults.Created($"/{result.Data!.Id}", result);
        }
    }
}

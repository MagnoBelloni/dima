using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                .WithName("Transactions: Get By Period")
                .WithSummary("Obtem todas as transações por filtro de tempo de um usuário")
                .Produces<PagedResponse<List<Transaction>>>();
        }

        private static async Task<IResult> HandleAsync(ITransactionHandler handler,
            ClaimsPrincipal claimsPrincipal,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var request = new GetTransactionByPeriodRequest()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                EndDate = endDate,
                StartDate = startDate,
                UserId = claimsPrincipal.Identity?.Name ?? string.Empty
            };

            var result = await handler.GetByPeriodAsync(request);
            if (!result.IsSuccess)
                return TypedResults.BadRequest(result);

            return TypedResults.Ok(result);
        }
    }
}

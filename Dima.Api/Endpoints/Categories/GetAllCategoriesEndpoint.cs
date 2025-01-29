using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core;
using Dima.Core.BaseResponses;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                .WithName("Categories: Get All")
                .WithSummary("Obtem todas as categoria de um usuário")
                .Produces<PagedResponse<List<Category>>>();
        }

        private static async Task<IResult> HandleAsync(ICategoryHandler handler,
            ClaimsPrincipal claimsPrincipal,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCategoriesRequest()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                UserId = claimsPrincipal.Identity?.Name ?? string.Empty,
            };

            var result = await handler.GetAllAsync(request);
            if (!result.IsSuccess)
                return TypedResults.BadRequest(result);

            return TypedResults.Ok(result);
        }
    }
}

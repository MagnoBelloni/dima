using Dima.Api.Common.Api;
using Dima.Core.BaseResponses;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Categories;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync)
               .WithName("Categories: Get By Id")
               .WithSummary("Obtem uma categoria")
               .Produces<BaseResponse<Category?>>();
        }

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id, ClaimsPrincipal claimsPrincipal)
        {
            var request = new GetCategoryByIdRequest(id)
            {
                UserId = claimsPrincipal.Identity?.Name ?? string.Empty,
            };

            var result = await handler.GetByIdAsync(request);
            if (!result.IsSuccess)
                return TypedResults.BadRequest(result);

            return TypedResults.Ok(result);
        }
    }
}

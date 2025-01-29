using Dima.Api.Common.Api;
using Dima.Core.BaseResponses;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Categories;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", HandleAsync)
               .WithName("Categories: Delete")
               .WithSummary("Deleta uma categoria")
               .Produces<BaseResponse<Category?>>();
        }

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id, ClaimsPrincipal claimsPrincipal)
        {
            var request = new DeleteCategoryRequest(id)
            {
                UserId = claimsPrincipal.Identity?.Name ?? string.Empty
            };

            var result = await handler.DeleteAsync(request);
            if (!result.IsSuccess)
                return TypedResults.BadRequest(result);

            return TypedResults.Ok(result);
        }
    }
}

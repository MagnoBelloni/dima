using Dima.Api.Common.Api;
using Dima.Core.BaseResponses;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Categories;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", HandleAsync)
               .WithName("Categories: Update")
               .WithSummary("Atualizar uma categoria")
               .Produces<BaseResponse<Category?>>();
        }

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler, UpdateCategoryRequest request, long id, ClaimsPrincipal claimsPrincipal)
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

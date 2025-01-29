using Dima.Api.Common.Api;
using Dima.Core.BaseResponses;
using Dima.Core.Handlers;
using Dima.Core.Model;
using Dima.Core.Requests.Categories;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("Categories: Create")
                .WithSummary("Criar uma nova categoria")
                .Produces<BaseResponse<Category?>>();

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler, CreateCategoryRequest request, ClaimsPrincipal claimsPrincipal)
        {
            request.UserId = claimsPrincipal.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);
            if (!result.IsSuccess)
                return TypedResults.BadRequest(result);

            return TypedResults.Created($"/{result.Data!.Id}", result);
        }
    }
}

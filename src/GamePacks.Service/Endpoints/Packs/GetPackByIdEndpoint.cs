using GamePacks.Service.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GamePacks.Service.Endpoints;

public static class GetPackByIdEndpoint
{
    public static void MapGetPackByIdEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
        .MapGet("pack/{id:guid}", HandleAsync)
        .AllowAnonymous()
        .WithName("GetPackById")
        .WithOpenApi()
        .WithDescription("Get a pack and its contents given its unique Guid");
    }

    public static async Task<Results<Ok<GetPackByIdResponse>, ProblemHttpResult>> HandleAsync(
        [FromRoute] Guid id, 
        [FromServices] GetPackByIdQueryHandler queryHandler)
    {
        var result = await queryHandler.ExecuteAsync(id);

       return result.Match<Results<Ok<GetPackByIdResponse>, ProblemHttpResult>>(
            pack => 
            {
                var packResponse = new GetPackByIdResponse() 
                {
                    Id = pack.Id,
                    PackName = pack.Name,
                    Active = pack.IsActive,
                    Price = pack.Price,
                    Content = pack.PackItems?.Select(x=>x.Name) ?? [],
                    ChildPackIds = pack.ChildPacks.Select(x => x.Name)
                };

                return TypedResults.Ok(packResponse);
            },
            error => 
            {
                return TypedResults.Problem(
                    detail: error.ToString(),
                    statusCode: 500, 
                    title: "Unexpected error getting pack");
            }
        );
    }
}

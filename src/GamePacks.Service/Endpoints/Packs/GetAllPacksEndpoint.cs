using GamePacks.Service.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GamePacks.Service.Endpoints;

public static class GetAllPacksEndpoint
{
    public static void MapGetAllPacksEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
        .MapGet("pack", HandleAsync)
        .AllowAnonymous()
        .WithName("GetAllPacks")
        .WithOpenApi()
        .WithDescription("Get names of all the packs in the database");
    }

    public static async Task<Results<Ok<GetAllPacksResponse>, ProblemHttpResult>> HandleAsync(
        [FromServices] GetAllPacksQueryHandler queryHandler)
    {
        var result = await queryHandler.ExecuteAsync();

       return result.Match<Results<Ok<GetAllPacksResponse>, ProblemHttpResult>>(
            packs => 
            {
                var allPackNames = packs.Select(p => p.Name);

                return TypedResults.Ok(new GetAllPacksResponse{ AllPackNames = allPackNames});
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

using GamePacks.Service.Models.Errors;
using GamePacks.Service.Models.Responses;
using GamePacks.Service.UseCases.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GamePacks.Service.Endpoints;

public static class GetAllPacksEndpoint
{
    public static void MapGetAllPacksEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
        .MapGet("packs", HandleAsync)
        .AllowAnonymous()
        .WithName("GetAllPacks")
        .WithOpenApi()
        .WithDescription("Get names of all the packs in the database");
    }

    public static async Task<Results<Ok<GetAllPacksResponse>, ProblemHttpResult>> HandleAsync(
        [FromServices] GetAllPacksQueryHandler queryHandler,
        CancellationToken cancellationToken)
    {
        var result = await queryHandler.ExecuteAsync(cancellationToken);

        return result.Match<Results<Ok<GetAllPacksResponse>, ProblemHttpResult>>(
             packs =>
             {
                 var allPackNames = packs.Select(p => p.Name);

                 return TypedResults.Ok(new GetAllPacksResponse { AllPackNames = allPackNames });
             },
             error =>
             {
                 if (error is PackNotFoundError)
                     return TypedResults.Problem(
                         detail: error.Message,
                         statusCode: 404,
                         title: "No packs found");

                 return TypedResults.Problem(
                     detail: error.Message,
                     statusCode: 500,
                     title: "Unexpected error getting pack");
             }
         );
    }
}

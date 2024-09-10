using GamePacks.Service.UseCases;
using GamePacks.Service.Models;
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
        [FromServices] GetPackByIdQueryHandler queryHandler,
        CancellationToken cancellationToken)
    {
        var result = await queryHandler.ExecuteAsync(id,cancellationToken);

       return result.Match<Results<Ok<GetPackByIdResponse>, ProblemHttpResult>>(
            pack => 
            {
                var packResponse = pack.MapPackToResponse();
                return TypedResults.Ok(packResponse);
            },
            error => 
            {
                 if (error is PackNotFoundError)
                     return TypedResults.Problem(
                         detail: error.Message,
                         statusCode: 404,
                         title: "Not found");

                 return TypedResults.Problem(
                     detail: error.Message,
                     statusCode: 500,
                     title: "Unexpected error getting pack");
            }
        );
    }
}

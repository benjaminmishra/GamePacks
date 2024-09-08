using GamePacks.Service.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GamePacks.Service.Endpoints;

public static class CreatePackEndpoint
{
    public static void MapCreatePackEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
        .MapPost("pack", HandleAsync)
        .AllowAnonymous()
        .WithName("CreatePack")
        .WithOpenApi()
        .WithDescription("Creates pack and its contents. Also allows to attach child packs based on their ids");
    }

    public static async Task<Results<Ok<CreatePackResponse>, ProblemHttpResult>> HandleAsync(
        CreatePackRequest request, 
        [FromServices] CreatePackCommandHandler commandHandler)
    {
        var result = await commandHandler.ExecuteAsync(request);

       return result.Match<Results<Ok<CreatePackResponse>, ProblemHttpResult>>(
            pack => 
            {
                return TypedResults.Ok(new CreatePackResponse());
            },
            error => 
            {
                return TypedResults.Problem(
                    detail: error.ToString(),
                    statusCode: 500, 
                    title: "Unexpected error creating pack");
            }
        );
    }
}

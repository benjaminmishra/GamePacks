using GamePacks.Service.UseCases;
using GamePacks.Service.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GamePacks.Service.Endpoints;

public static class CreatePackEndpoint
{
    public static void MapCreatePackEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
        .MapPost("packs", HandleAsync)
        .AllowAnonymous()
        .WithName("CreatePack")
        .WithOpenApi()
        .WithDescription("Creates pack and its contents. Also allows to attach child packs based on their ids");
    }

    public static async Task<Results<Ok<CreatePackResponse>, BadRequest<string>, ProblemHttpResult>> HandleAsync(
        [FromBody] CreatePackRequest request,
        [FromServices] CreatePackCommandHandler commandHandler,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.PackName))
            return TypedResults.BadRequest("Pack name cannot be null empty or whitespace");

        var result = await commandHandler.ExecuteAsync(request, cancellationToken);

        return result.Match<Results<Ok<CreatePackResponse>, BadRequest<string>, ProblemHttpResult>>(
             pack =>
             {
                 return TypedResults.Ok(
                    new CreatePackResponse
                    {
                        Id = pack.Id,
                        PackName = pack.Name,
                        Active = pack.IsActive,
                        Price = pack.Price,
                        ChildPackIds = pack.ChildPacks.Select(x => x.Id).ToArray(),
                        Contents = pack.PackItems.Select(x => x.Name).ToArray()
                    });
             },
             error =>
             {
                 if (error is PackValidationError)
                     return TypedResults.Problem(
                        detail: error.Message,
                        statusCode: 400,
                        title: "Create Pack request validation error");

                 return TypedResults.Problem(
                  detail: error.Message,
                  statusCode: 500,
                  title: "Unexpected error creating pack");
             }
         );
    }
}

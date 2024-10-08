namespace GamePacks.Service.Endpoints;

public static class Routes
{
    public static IEndpointRouteBuilder RegisterV1Endpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("/api/v1").WithOpenApi();

        // Map all routes
        group.MapCreatePackEndpoint();
        group.MapGetPackByIdEndpoint();
        group.MapGetAllPacksEndpoint();

        return group;
    }
}
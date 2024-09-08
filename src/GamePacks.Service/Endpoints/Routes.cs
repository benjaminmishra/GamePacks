namespace GamePacks.Service.Endpoints;

public static class Routes
{
    public static IEndpointRouteBuilder RegisterV1Endpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("/api/v1");

        // Map all routes
        group.MapCreatePackEndpoint();

        return group;
    }
}
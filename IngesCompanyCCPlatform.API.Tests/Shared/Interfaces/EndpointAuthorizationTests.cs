using System.Reflection;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace IngesCompanyCCPlatform.API.Tests.Shared.Interfaces;

/// <summary>
///     Checks that every endpoint of the RESTful API needs a token, except the public ones listed here: the sign-in and
///     sign-up, the Contact us form, the telemetry that ThingsBoard sends with its API key, the plans and the calls of the
///     registration of a new organization.
/// </summary>
public class EndpointAuthorizationTests
{
    private static readonly string[] PublicEndpoints =
    [
        "AuthenticationController.SignIn",
        "AuthenticationController.SignUp",
        "ContactInquiriesController.SendContactInquiry",
        "FacilitiesController.AddFacility",
        "OrganizationsController.GetAllOrganizations",
        "OrganizationsController.RegisterOrganization",
        "PlansController.GetAllPlans",
        "PlansController.GetPlanById",
        "SubscriptionsController.CreateSubscription",
        "TelemetryController.ReceiveTelemetry",
        "TokensController.CreateToken",
        "UsersController.CreateUser"
    ];

    private static IEnumerable<MethodInfo> Endpoints() =>
        typeof(Program).Assembly.GetTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract)
            .SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            .Where(method => method.GetCustomAttributes<HttpMethodAttribute>().Any());

    private static string NameOf(MethodInfo method) => $"{method.DeclaringType!.Name}.{method.Name}";

    private static bool IsPublic(MethodInfo method) => method.GetCustomAttribute<AllowAnonymousAttribute>() is not null;

    private static bool NeedsToken(MethodInfo method) =>
        method.GetCustomAttributes<AuthorizeAttribute>().Any() ||
        method.DeclaringType!.GetCustomAttributes<AuthorizeAttribute>().Any();

    [Fact]
    public void EveryEndpointNeedsATokenOrIsMarkedAsPublic()
    {
        var unprotected = Endpoints().Where(method => !IsPublic(method) && !NeedsToken(method)).Select(NameOf).ToList();

        Assert.Empty(unprotected);
    }

    [Fact]
    public void OnlyTheListedEndpointsArePublic()
    {
        var publicEndpoints = Endpoints().Where(IsPublic).Select(NameOf).Order().ToArray();

        Assert.Equal(PublicEndpoints.Order().ToArray(), publicEndpoints);
    }

    [Fact]
    public void TheApiHasEndpointsToCheck()
    {
        Assert.True(Endpoints().Count() > 100);
    }
}

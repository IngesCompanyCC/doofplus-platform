using System.Net.Http.Headers;
using System.Net.Http.Json;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IngesCompanyCCPlatform.API.Tests.Shared.Infrastructure;

/// <summary>
///     Runs the whole RESTful API in memory for the integration tests: the real controllers, middleware and IAM, with
///     an in-memory database instead of MySQL. Every factory gets its own empty database.
/// </summary>
public class PlatformApiFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseName = $"doofplus-tests-{Guid.NewGuid()}";
    private int _lastOrganizationId;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.RemoveAll(typeof(IDbContextOptionsConfiguration<AppDbContext>));
            services.AddDbContext<AppDbContext>((serviceProvider, options) => options
                .UseInMemoryDatabase(_databaseName)
                .AddInterceptors(serviceProvider.GetRequiredService<DispatchDomainEventsInterceptor>()));
        });
    }

    /// <summary>
    ///     Signs up the first user of a new organization (its administrator) and returns a client that sends its token.
    ///     Each call uses another organization, because a second user of the same organization needs an invitation.
    /// </summary>
    public async Task<HttpClient> CreateAuthenticatedClientAsync(string email = "carlos.medina@andinos.com.pe")
    {
        var client = CreateClient();
        const string password = "DoofPlus2026!";

        var signUp = await client.PostAsJsonAsync("/api/v1/authentication/sign-up", new
        {
            email,
            password,
            fullName = "Carlos Medina",
            environment = "administration",
            organizationId = Interlocked.Increment(ref _lastOrganizationId),
            organizationName = "Laboratorios Andinos",
            plant = "Ate plant"
        });
        signUp.EnsureSuccessStatusCode();

        var signIn = await client.PostAsJsonAsync("/api/v1/authentication/sign-in", new { email, password });
        signIn.EnsureSuccessStatusCode();
        var authenticated = await signIn.Content.ReadFromJsonAsync<TokenResponse>();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticated!.Token);
        return client;
    }

    private sealed record TokenResponse(string Token);
}

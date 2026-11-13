using IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Interfaces.Asp.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Interfaces.Asp.OpenApi;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Localization.Extensions;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Mediator.Cortex.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Monitoring.Application.CommandServices;
using IngesCompanyCCPlatform.API.Monitoring.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Monitoring.Application.Internal.QueryServices;
using IngesCompanyCCPlatform.API.Monitoring.Application.QueryServices;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;
using IngesCompanyCCPlatform.API.Monitoring.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Monitoring.Resources;
using IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Application.Internal.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;
using IngesCompanyCCPlatform.API.Organizations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Pipeline.Middleware.Components;
using IngesCompanyCCPlatform.API.Shared.Resources;
using IngesCompanyCCPlatform.API.Subscriptions.Application.CommandServices;
using IngesCompanyCCPlatform.API.Subscriptions.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Subscriptions.Application.Internal.QueryServices;
using IngesCompanyCCPlatform.API.Subscriptions.Application.QueryServices;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Repositories;
using IngesCompanyCCPlatform.API.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Subscriptions.Resources;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Scalar.AspNetCore;
using ProblemDetailsFactory = IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure Kebab Case Route Naming Convention
builder.Services.AddControllers(options =>
        options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Add ProblemDetails and the global exception handler
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// OpenAPI / Scalar documentation
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

// Add Database Connection
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .AddInterceptors(serviceProvider.GetRequiredService<DispatchDomainEventsInterceptor>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Localization: the resources (.resx) live next to their marker classes in Shared/Resources,
// so no ResourcesPath is set; with ResourcesPath = "Resources" the messages are not found.
builder.Services.AddLocalization();

// Register the custom ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory>();

// Configure Dependency Injection

// Organizations Bounded Context
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
builder.Services.AddScoped<IContactInquiryRepository, ContactInquiryRepository>();

builder.Services.AddScoped<IOrganizationCommandService, OrganizationCommandService>();
builder.Services.AddScoped<IUserProfileCommandService, UserProfileCommandService>();
builder.Services.AddScoped<IContactInquiryCommandService, ContactInquiryCommandService>();
builder.Services.AddScoped<IOrganizationQueryService, OrganizationQueryService>();
builder.Services.AddScoped<IUserProfileQueryService, UserProfileQueryService>();
builder.Services.AddScoped<IContactInquiryQueryService, ContactInquiryQueryService>();

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DispatchDomainEventsInterceptor>();

// Subscriptions Bounded Context Injection Configuration
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IPlanCommandService, PlanCommandService>();
builder.Services.AddScoped<IPlanQueryService, PlanQueryService>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();
builder.Services.AddSingleton<IStringLocalizer<SubscriptionsMessages>, StringLocalizer<SubscriptionsMessages>>();

// Localized messages of the bounded contexts. Each bounded context adds one line with its marker class;
// the global exception handler uses them to translate the message keys of the value objects.
// Example: builder.Services.AddBoundedContextMessages<SubscriptionsMessages>();
builder.Services.AddBoundedContextMessages<CommonMessages>();
builder.Services.AddBoundedContextMessages<SubscriptionsMessages>();
builder.Services.AddBoundedContextMessages<MonitoringMessages>();

// Manufacturing Bounded Context Injection Configuration
builder.Services.AddScoped<IProductionBatchRepository, ProductionBatchRepository>();
builder.Services.AddScoped<IProductionBatchCommandService, ProductionBatchCommandService>();
builder.Services.AddScoped<IProductionBatchQueryService, ProductionBatchQueryService>();

// Monitoring Bounded Context Injection Configuration
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<IReadingRepository, ReadingRepository>();
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IEquipmentCommandService, EquipmentCommandService>();
builder.Services.AddScoped<IEquipmentQueryService, EquipmentQueryService>();
builder.Services.AddScoped<ISensorCommandService, SensorCommandService>();
builder.Services.AddScoped<ISensorQueryService, SensorQueryService>();
builder.Services.AddScoped<IReadingCommandService, ReadingCommandService>();
builder.Services.AddScoped<IReadingQueryService, ReadingQueryService>();
builder.Services.AddScoped<IAlertCommandService, AlertCommandService>();
builder.Services.AddScoped<IAlertQueryService, AlertQueryService>();

// Mediator Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(ExceptionHandlingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(TransactionCommandBehavior<>));

// Add Cortex Mediator for commands and domain event handling
builder.Services.AddCortexMediator([typeof(Program)]);

var app = builder.Build();

// Apply pending migrations on startup (safe to call even when schema is up to date)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.

// Request localization goes first, so the global exception handler answers in the request language.
var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapScalarApiReference();

app.Run();

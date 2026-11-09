using IngesCompanyCCPlatform.API.Quality.Application.Acl;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Acl;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Interfaces.Asp.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Interfaces.Asp.OpenApi;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Mediator.Cortex.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Pipeline.Middleware.Components;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
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

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DispatchDomainEventsInterceptor>();

// Quality & Compliance Bounded Context Injection Configuration
builder.Services.AddScoped<IQualityDocumentRepository, QualityDocumentRepository>();
builder.Services.AddScoped<IMaterialApprovalRepository, MaterialApprovalRepository>();
builder.Services.AddScoped<IAnalyticalResultRepository, AnalyticalResultRepository>();
builder.Services.AddScoped<IBatchReviewRepository, BatchReviewRepository>();
builder.Services.AddScoped<IDeviationRepository, DeviationRepository>();
builder.Services.AddScoped<IAuditRepository, AuditRepository>();
builder.Services.AddScoped<IAuditTrailEntryRepository, AuditTrailEntryRepository>();
builder.Services.AddScoped<IRegulatoryReportRepository, RegulatoryReportRepository>();
builder.Services.AddScoped<IQualityDocumentCommandService, QualityDocumentCommandService>();
builder.Services.AddScoped<IMaterialApprovalCommandService, MaterialApprovalCommandService>();
builder.Services.AddScoped<IAnalyticalResultCommandService, AnalyticalResultCommandService>();
builder.Services.AddScoped<IBatchReviewCommandService, BatchReviewCommandService>();
builder.Services.AddScoped<IDeviationCommandService, DeviationCommandService>();
builder.Services.AddScoped<IAuditCommandService, AuditCommandService>();
builder.Services.AddScoped<IAuditTrailEntryCommandService, AuditTrailEntryCommandService>();
builder.Services.AddScoped<IRegulatoryReportCommandService, RegulatoryReportCommandService>();
builder.Services.AddScoped<IQualityDocumentQueryService, QualityDocumentQueryService>();
builder.Services.AddScoped<IMaterialApprovalQueryService, MaterialApprovalQueryService>();
builder.Services.AddScoped<IAnalyticalResultQueryService, AnalyticalResultQueryService>();
builder.Services.AddScoped<IBatchReviewQueryService, BatchReviewQueryService>();
builder.Services.AddScoped<IDeviationQueryService, DeviationQueryService>();
builder.Services.AddScoped<IAuditQueryService, AuditQueryService>();
builder.Services.AddScoped<IAuditTrailEntryQueryService, AuditTrailEntryQueryService>();
builder.Services.AddScoped<IRegulatoryReportQueryService, RegulatoryReportQueryService>();
builder.Services.AddScoped<IQualityContextFacade, QualityContextFacade>();

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

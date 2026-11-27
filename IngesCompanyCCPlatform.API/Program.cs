using IngesCompanyCCPlatform.API.Iam.Application.Acl;
using IngesCompanyCCPlatform.API.Iam.Application.CommandServices;
using IngesCompanyCCPlatform.API.Iam.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Iam.Application.Internal.OutboundServices;
using IngesCompanyCCPlatform.API.Iam.Application.Internal.QueryServices;
using IngesCompanyCCPlatform.API.Iam.Application.QueryServices;
using IngesCompanyCCPlatform.API.Iam.Domain.Repositories;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Hashing.BCrypt.Services;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Tokens.Jwt.Configuration;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Tokens.Jwt.Services;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Acl;
using IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Manufacturing.Resources;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Quality.Resources;
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
using IngesCompanyCCPlatform.API.Subscriptions.Application.OutboundServices.PaymentGateway;
using IngesCompanyCCPlatform.API.Subscriptions.Infrastructure.PaymentGateway.Niubiz;
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
// Only the Web Application and the Website of the product may call the API (Cors:AllowedOrigins, or Cors__AllowedOrigins__0, __1… as environment variables)
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy("ProductOriginsPolicy",
        policy => policy.WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// OpenAPI / Scalar documentation
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
    options.AddOperationTransformer<IamBearerSecurityOperationTransformer>();
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
builder.Services.AddScoped<IAdministrativeActivityRepository, AdministrativeActivityRepository>();

builder.Services.AddScoped<IOrganizationCommandService, OrganizationCommandService>();
builder.Services.AddScoped<IUserProfileCommandService, UserProfileCommandService>();
builder.Services.AddScoped<IContactInquiryCommandService, ContactInquiryCommandService>();
builder.Services.AddScoped<IOrganizationQueryService, OrganizationQueryService>();
builder.Services.AddScoped<IUserProfileQueryService, UserProfileQueryService>();
builder.Services.AddScoped<IContactInquiryQueryService, ContactInquiryQueryService>();
builder.Services.AddScoped<IFacilityQueryService, FacilityQueryService>();
builder.Services.AddScoped<IAdministrativeActivityQueryService, AdministrativeActivityQueryService>();

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context (JWT secret in TokenSettings:Secret; in production set TokenSettings__Secret)
var tokenSecret = builder.Configuration["TokenSettings:Secret"];
if (string.IsNullOrWhiteSpace(tokenSecret) || tokenSecret.Length < 32)
    throw new InvalidOperationException(
        "TokenSettings:Secret is missing or shorter than 32 characters. Set the TokenSettings__Secret environment variable.");
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();
builder.Services.AddScoped<IInvitationCommandService, InvitationCommandService>();
builder.Services.AddScoped<IInvitationQueryService, InvitationQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
builder.Services.AddScoped<DispatchDomainEventsInterceptor>();

// Subscriptions Bounded Context Injection Configuration
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IPlanCommandService, PlanCommandService>();
builder.Services.AddScoped<IPlanQueryService, PlanQueryService>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.Configure<NiubizSettings>(builder.Configuration.GetSection("Niubiz"));
builder.Services.AddHttpClient<IPaymentGateway, NiubizPaymentGateway>(client => client.Timeout = TimeSpan.FromSeconds(30));
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();
builder.Services.AddSingleton<IStringLocalizer<SubscriptionsMessages>, StringLocalizer<SubscriptionsMessages>>();

// Localized messages of the bounded contexts. Each bounded context adds one line with its marker class;
// the global exception handler uses them to translate the message keys of the value objects.
// Example: builder.Services.AddBoundedContextMessages<SubscriptionsMessages>();
builder.Services.AddBoundedContextMessages<CommonMessages>();
builder.Services.AddBoundedContextMessages<SubscriptionsMessages>();
builder.Services.AddBoundedContextMessages<ManufacturingMessages>();
builder.Services.AddBoundedContextMessages<MonitoringMessages>();
builder.Services.AddBoundedContextMessages<QualityMessages>();
// Quality & Compliance Bounded Context Injection Configuration
builder.Services.AddScoped<IQualityDocumentRepository, QualityDocumentRepository>();
builder.Services.AddScoped<IDeviationRepository, DeviationRepository>();
builder.Services.AddScoped<IEvidenceRepository, EvidenceRepository>();
builder.Services.AddScoped<ICapaPlanRepository, CapaPlanRepository>();
builder.Services.AddScoped<ICapaActionRepository, CapaActionRepository>();
builder.Services.AddScoped<IBatchReleaseRepository, BatchReleaseRepository>();
builder.Services.AddScoped<IAnalyticalResultRepository, AnalyticalResultRepository>();
builder.Services.AddScoped<IAuditRepository, AuditRepository>();
builder.Services.AddScoped<IAuditFindingRepository, AuditFindingRepository>();
builder.Services.AddScoped<IRegulatoryReportRepository, RegulatoryReportRepository>();
builder.Services.AddScoped<ICollaborationTaskRepository, CollaborationTaskRepository>();
builder.Services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
builder.Services.AddScoped<ITraceabilityGapRepository, TraceabilityGapRepository>();
builder.Services.AddScoped<IAuditEventRepository, AuditEventRepository>();
builder.Services.AddScoped<IQualityDocumentCommandService, QualityDocumentCommandService>();
builder.Services.AddScoped<IQualityDocumentQueryService, QualityDocumentQueryService>();
builder.Services.AddScoped<IDeviationCommandService, DeviationCommandService>();
builder.Services.AddScoped<IDeviationQueryService, DeviationQueryService>();
builder.Services.AddScoped<IEvidenceCommandService, EvidenceCommandService>();
builder.Services.AddScoped<IEvidenceQueryService, EvidenceQueryService>();
builder.Services.AddScoped<ICapaPlanCommandService, CapaPlanCommandService>();
builder.Services.AddScoped<ICapaPlanQueryService, CapaPlanQueryService>();
builder.Services.AddScoped<ICapaActionCommandService, CapaActionCommandService>();
builder.Services.AddScoped<ICapaActionQueryService, CapaActionQueryService>();
builder.Services.AddScoped<IBatchReleaseCommandService, BatchReleaseCommandService>();
builder.Services.AddScoped<IBatchReleaseQueryService, BatchReleaseQueryService>();
builder.Services.AddScoped<IAnalyticalResultCommandService, AnalyticalResultCommandService>();
builder.Services.AddScoped<IAnalyticalResultQueryService, AnalyticalResultQueryService>();
builder.Services.AddScoped<IAuditCommandService, AuditCommandService>();
builder.Services.AddScoped<IAuditQueryService, AuditQueryService>();
builder.Services.AddScoped<IAuditFindingCommandService, AuditFindingCommandService>();
builder.Services.AddScoped<IAuditFindingQueryService, AuditFindingQueryService>();
builder.Services.AddScoped<IRegulatoryReportCommandService, RegulatoryReportCommandService>();
builder.Services.AddScoped<IRegulatoryReportQueryService, RegulatoryReportQueryService>();
builder.Services.AddScoped<ICollaborationTaskCommandService, CollaborationTaskCommandService>();
builder.Services.AddScoped<ICollaborationTaskQueryService, CollaborationTaskQueryService>();
builder.Services.AddScoped<ITaskCommentCommandService, TaskCommentCommandService>();
builder.Services.AddScoped<ITaskCommentQueryService, TaskCommentQueryService>();
builder.Services.AddScoped<ITraceabilityGapCommandService, TraceabilityGapCommandService>();
builder.Services.AddScoped<ITraceabilityGapQueryService, TraceabilityGapQueryService>();
builder.Services.AddScoped<IAuditEventCommandService, AuditEventCommandService>();
builder.Services.AddScoped<IAuditEventQueryService, AuditEventQueryService>();
builder.Services.AddScoped<IDeviationTrendQueryService, DeviationTrendQueryService>();

// Manufacturing Bounded Context Injection Configuration
builder.Services.AddScoped<IProductionBatchRepository, ProductionBatchRepository>();
builder.Services.AddScoped<IProductionBatchCommandService, ProductionBatchCommandService>();
builder.Services.AddScoped<IProductionBatchQueryService, ProductionBatchQueryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMasterFormulaRepository, MasterFormulaRepository>();
builder.Services.AddScoped<IProductionOrderRepository, ProductionOrderRepository>();
builder.Services.AddScoped<IRawMaterialLotRepository, RawMaterialLotRepository>();
builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();
builder.Services.AddScoped<IMasterFormulaCommandService, MasterFormulaCommandService>();
builder.Services.AddScoped<IMasterFormulaQueryService, MasterFormulaQueryService>();
builder.Services.AddScoped<IProductionOrderCommandService, ProductionOrderCommandService>();
builder.Services.AddScoped<IProductionOrderQueryService, ProductionOrderQueryService>();
builder.Services.AddScoped<IRawMaterialLotCommandService, RawMaterialLotCommandService>();
builder.Services.AddScoped<IRawMaterialLotQueryService, RawMaterialLotQueryService>();

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

// The OpenAPI document is served in every environment, so the Scalar reference also works once deployed.
app.MapOpenApi();

// Apply CORS Policy
app.UseCors("ProductOriginsPolicy");

// Reads the Bearer token; the IAM [Authorize] attribute answers 401 or 403
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapScalarApiReference();

app.Run();

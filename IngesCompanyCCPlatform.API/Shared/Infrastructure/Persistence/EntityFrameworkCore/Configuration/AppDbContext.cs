using Humanizer;
using IngesCompanyCCPlatform.API.Shared.Infrastracture.Persistence.EntityFrameworkCore.Configuration.Extensions;
using IngesCompanyCCPlatform.API.Shared.Infrastracture.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Shared.Infrastracture.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.UseSnakeCaseNamingConvention();
    }
}
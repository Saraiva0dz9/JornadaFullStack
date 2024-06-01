using Final.Api.Data;
using Final.Api.Services;
using Final.Core;
using Final.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Final.Api.Common.API; 

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("Default") ?? string.Empty;
        Configurations.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configurations.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => 
        { 
            x.CustomSchemaIds(y => y.FullName); 
        });
    }

    public static void AddDataContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>
            (options => options.UseSqlServer(ApiConfiguration.ConnectionString));
    }

    public static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(ApiConfiguration.CorsPolicyName, policy =>
            {
                policy.WithOrigins(Configurations.BackendUrl, Configurations.FrontendUrl)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<ITransactionService, TransactionService>();
    }
}

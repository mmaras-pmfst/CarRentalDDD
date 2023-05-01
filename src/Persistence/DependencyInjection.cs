using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {

        string connectionString = configuration.GetConnectionString("Database")!;


        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));


        return services;
    }
}
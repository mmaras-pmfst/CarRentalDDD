using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {

        string connectionString = configuration.GetConnectionString("Database")!;


        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IOfficeRepository, OfficeRepository>();
        services.AddScoped<IColorRepository, ColorRepository>();
        services.AddScoped<ICarCategoryRepository, CarCategoryRepository>();
        services.AddScoped<ICarBrandRepository, CarBrandRepository>();
        services.AddScoped<ICarModelRepository, CarModelRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IExtrasRepository, ExtrasRepository>();
        services.AddScoped<IWorkerRepository, WorkerRepository>();
        services.AddScoped<ICarModelRentRepository, CarModelRentRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IReservationItemRepository, ReservationDetailRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();


        return services;
    }
}
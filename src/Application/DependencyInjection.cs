using Application.Behaviors;
using Application.Mappings.Configurations;
using AutoMapper;
using FluentValidation;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
        });
        var mapperConfig = new AutoMapperConfiguration().Configure();
        IMapper mapper = mapperConfig.CreateMapper();

        services.AddSingleton(mapper);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly,
            includeInternalTypes: true);

        return services;
    }
}
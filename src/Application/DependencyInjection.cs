using Application.Behaviors;
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

            // ForeachAwaitPublisher approach: it just iterates over all off the handlers for the given notification and then
            // it invokes them one by one 
            //config.NotificationPublisher = new ForeachAwaitPublisher();

            // TaskWhenAllPublisher approach: iterate over the executers but you just select the task returned by the handler callback
            // and you pass them in array. After that you call Task.WhenAll() which is going to await all of the tasks at the same time.
            // This includes some degree of parallelisation
            //config.NotificationPublisher = new TaskWhenAllPublisher();
        });

        //services.AddValidatorsFromAssembly(assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly,
            includeInternalTypes: true);

        return services;
    }
}
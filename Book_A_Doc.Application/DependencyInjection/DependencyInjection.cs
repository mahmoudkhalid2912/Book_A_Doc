using Book_A_Doc.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Book_A_Doc.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add FluentValidation validators from the current assembly
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);


        // Add MediatR and register the ValidationPipelineBehavior
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });



        

        return services;
    }

    
}
using Book_A_Doc.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Book_A_Doc.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly);

            cfg.AddOpenBehavior(
                typeof(ValidationPipelineBehavior<,>));

            cfg.AddOpenBehavior(
                typeof(TransactionBehavior<,>));
        });

        return services;
    }
}
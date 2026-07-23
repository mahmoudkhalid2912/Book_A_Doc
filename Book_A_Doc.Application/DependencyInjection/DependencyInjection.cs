using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Book_A_Doc.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add MediatR Services
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));



        //Add FluentValidation Services
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);



        //

        return services;
    }

    
}
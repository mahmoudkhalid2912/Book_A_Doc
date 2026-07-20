using Book_A_Doc.Infrastructre.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddDbContext<Book_A_Doc_Context>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("ConnectionString"));
        });

       

        return services;
    }
}
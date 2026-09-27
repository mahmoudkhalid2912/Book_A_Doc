using System.Text.Json.Serialization;

namespace Book_A_Doc.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
      .AddControllers()
      .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
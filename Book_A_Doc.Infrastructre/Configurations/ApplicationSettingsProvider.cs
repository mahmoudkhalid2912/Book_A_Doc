using Book_A_Doc.Application.Services;
using Microsoft.Extensions.Options;

namespace Book_A_Doc.Infrastructre.Configurations;

public class ApplicationSettingsProvider(
    IOptions<ApplicationSettings> options)
    : IApplicationSettings
{
    public string BaseUrl => options.Value.BaseUrl;
}
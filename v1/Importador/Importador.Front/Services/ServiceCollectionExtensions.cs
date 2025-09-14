// Services/ServiceCollectionExtensions.cs
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Importador.Front.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackendApi(this IServiceCollection services, Uri baseAddress)
    {
        services.AddHttpClient("backend", c =>
        {
            c.BaseAddress = baseAddress;
            c.DefaultRequestHeaders.Accept.ParseAdd("application/json");

        });

        services.AddScoped<IImportsApi, ImportsApi>();
        services.AddScoped<IAlunosApi, AlunosApi>();
        services.AddScoped<IObservacoesApi, ObservacoesApi>();
        services.AddScoped<IAreasApi, AreasApi>();
        services.AddScoped<IDisciplinasApi, DisciplinasApi>();

        return services;
    }

    internal static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };
}

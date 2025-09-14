using System.Net.Http.Headers;
using System.Net.Http.Json;
using Importador.Front;
using Importador.Front.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

// ===== Blazor boot =====
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ===== Descobrir a URL da API =====
// 1) tenta ler wwwroot/appsettings.json (chave "ApiBaseUrl")
// 2) fallback para variável de ambiente "VITE_API_BASE" (ex.: via vite/parcel) ou "API_BASE_URL"
// 3) fallback final para http://localhost:5162/
string apiBase = "http://localhost:5155/";

try
{
    // Usa um HttpClient TEMPORÁRIO apontando para o próprio front
    using var tmp = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

    // Lê appsettings.json do wwwroot
    var cfg = await tmp.GetFromJsonAsync<Dictionary<string, string>>("appsettings.json");

    string? TryGet(params string[] keys)
        => cfg is null ? null : keys.Select(k => cfg.TryGetValue(k, out var v) ? v : null)
                                   .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v));

    var fromFile = TryGet("ApiBaseUrl", "API_BASE_URL");
    var fromEnv = Environment.GetEnvironmentVariable("API_BASE_URL");

    apiBase = fromFile ?? fromEnv ?? apiBase;
}
catch
{
    // Mantém fallback padrão se não achar/appsettings não existir
}

if (!apiBase.EndsWith("/")) apiBase += "/";

// ===== HttpClient único da aplicação, apontando para a API =====
builder.Services.AddScoped(sp =>
{
    var http = new HttpClient { BaseAddress = new Uri(apiBase) };
    http.DefaultRequestHeaders.Accept.Clear();
    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    return http;
});

// ===== DI dos API Clients (que usam rotas relativas: "api/...") =====
builder.Services.AddScoped<IAreasApi, AreasApi>();
builder.Services.AddScoped<IDisciplinasApi, DisciplinasApi>();
builder.Services.AddScoped<IObservacoesApi, ObservacoesApi>();
builder.Services.AddScoped<IImportsApi, ImportsApi>();
builder.Services.AddScoped<IAlunosApi, AlunosApi>();

await builder.Build().RunAsync();

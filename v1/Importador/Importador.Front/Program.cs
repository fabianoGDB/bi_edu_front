// Program.cs (Blazor WebAssembly - Client)
using System.Net.Http.Headers;
using Importador.Front;
using Importador.Front.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Root component
builder.RootComponents.Add<App>("#app");

// Carrega a URL do backend de wwwroot/appsettings.json
// Exemplo do arquivo:
// {
//   "Backend": {
//     "BaseUrl": "http://localhost:5162"
//   }
// }
var baseUrlRaw = builder.Configuration["Backend:BaseUrl"];

// Fallback (NÃO IDEAL): se não houver appsettings, usa a BaseAddress do próprio front
// Obs.: prefira SEMPRE definir Backend:BaseUrl no appsettings.json
if (string.IsNullOrWhiteSpace(baseUrlRaw))
{
    baseUrlRaw = builder.HostEnvironment.BaseAddress;
}

// Normaliza e valida URI
if (!Uri.TryCreate(NormalizeBaseUrl("http://localhost:5155"), UriKind.Absolute, out var backendBaseUri))
{
    // Falha de configuração visível no console e que não impede a app de subir
    Console.Error.WriteLine($"[Program] Backend:BaseUrl inválido: '{baseUrlRaw}'. " +
                            "Corrija em wwwroot/appsettings.json -> Backend.BaseUrl");
    backendBaseUri = new Uri(builder.HostEnvironment.BaseAddress); // evita null
}

// Logging (útil durante diagnóstico)
builder.Logging.SetMinimumLevel(LogLevel.Information);
Console.WriteLine($"[Program] Backend BaseUrl = {backendBaseUri}");

// HttpClient “default” (para arquivos estáticos do próprio front)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// HttpClient “backend” (para chamar a API)
builder.Services.AddHttpClient("backend", c =>
{
    c.BaseAddress = backendBaseUri;
    c.DefaultRequestHeaders.Accept.Clear();
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

// Registra seus services que usam o "backend"
builder.Services.AddScoped<IImportsApi, ImportsApi>();
builder.Services.AddScoped<IAlunosApi, AlunosApi>();
builder.Services.AddScoped<IObservacoesApi, ObservacoesApi>();

await builder.Build().RunAsync();

static string NormalizeBaseUrl(string url)
{
    // Remove espaços e garante barra final (http://host:porta/)
    url = url.Trim();
    if (!url.EndsWith("/")) url += "/";
    return url;
}

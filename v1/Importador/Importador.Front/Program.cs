using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Importador.Front;
using Importador.Front.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Base da API (ajuste para o seu backend)
var apiBase = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5155";

// HttpClient único para toda a app
builder.Services.AddScoped(sp =>
{
    var http = new HttpClient { BaseAddress = new Uri(apiBase), Timeout = TimeSpan.FromSeconds(100) };
    http.DefaultRequestHeaders.Accept.Clear();
    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    return http;
});

// Serviços de API
builder.Services.AddScoped<IImportsApi, ImportsApi>();
builder.Services.AddScoped<IAlunosApi, AlunosApi>();

await builder.Build().RunAsync();

// Program.cs  (exemplo)
using Importador.Front;
using Importador.Front.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Em appsettings.json:  "Backend": { "BaseUrl": "https://localhost:5001" }
var baseUrl = builder.Configuration["Backend:BaseUrl"]
              ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddBackendApi(new Uri(baseUrl));

await builder.Build().RunAsync();

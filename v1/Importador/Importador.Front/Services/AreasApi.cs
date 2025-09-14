using System.Net.Http.Json;
using System.Text.Json;
using Importador.Front.Models;

namespace Importador.Front.Services;

public sealed class AreasApi(HttpClient http) : IAreasApi
{

    private readonly HttpClient _http = http;
    static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);

    public async Task<List<AreaListItem>?> GetAllAsync()
    {
        var res = await _http.GetAsync("api/areas"); // sem leading slash, respeita BaseAddress
        var body = await res.Content.ReadAsStringAsync();

        if (!res.IsSuccessStatusCode)
            throw new HttpRequestException($"GET /api/areas => {(int)res.StatusCode} {res.ReasonPhrase}\n{body}");

        var ct = res.Content.Headers.ContentType?.MediaType ?? "";
        if (!ct.Contains("json", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException($"/api/areas devolveu Content-Type '{ct}'. Corpo:\n{body}");

        return JsonSerializer.Deserialize<List<AreaListItem>>(body,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public Task<AreaDto?> GetAsync(int id, CancellationToken ct = default)
        => _http.GetFromJsonAsync<AreaDto>($"/api/areas/{id}");

    public Task<AreaDto?> CreateAsync(AreaUpsert dto, CancellationToken ct = default)
        => _http.PostAsJsonAsync("/api/areas", dto, Json, ct)
               .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<AreaDto>(Json, ct)).Unwrap();

    public Task<AreaDto?> UpdateAsync(int id, AreaUpsert dto, CancellationToken ct = default)
        => _http.PutAsJsonAsync($"/api/areas/{id}", dto, Json, ct)
               .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<AreaDto>(Json, ct)).Unwrap();

    public Task<HttpResponseMessage> DeleteAsync(int id, CancellationToken ct = default)
        => _http.DeleteAsync($"/api/areas/{id}", ct);
}

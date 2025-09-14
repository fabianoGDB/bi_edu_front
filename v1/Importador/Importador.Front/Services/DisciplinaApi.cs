using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Importador.Front.Models;

namespace Importador.Front.Services;

public sealed class DisciplinasApi(HttpClient http) : IDisciplinasApi
{

    private readonly HttpClient _http = http;
    static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);

    public async Task<DisciplinaListPage?> GetPageAsync(int? areaId, string? q, int page, int pageSize, CancellationToken ct = default)
    {
        var sb = new StringBuilder("/api/disciplinas?");
        if (areaId is not null) sb.Append($"areaId={areaId}&");
        if (!string.IsNullOrWhiteSpace(q)) sb.Append($"q={Uri.EscapeDataString(q)}&");
        sb.Append($"page={page}&pageSize={pageSize}");
        return await _http.GetFromJsonAsync<DisciplinaListPage>(sb.ToString());
    }

    public Task<DisciplinaDto?> GetAsync(int id, CancellationToken ct = default)
        => _http.GetFromJsonAsync<DisciplinaDto>($"/api/disciplinas/{id}");

    public Task<DisciplinaDto?> CreateAsync(DisciplinaUpsert dto, CancellationToken ct = default)
        => _http.PostAsJsonAsync("/api/disciplinas", dto, Json, ct)
               .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<DisciplinaDto>(Json, ct)).Unwrap();

    public Task<DisciplinaDto?> UpdateAsync(int id, DisciplinaUpsert dto, CancellationToken ct = default)
        => _http.PutAsJsonAsync($"/api/disciplinas/{id}", dto, Json, ct)
               .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<DisciplinaDto>(Json, ct)).Unwrap();

    public Task<HttpResponseMessage> DeleteAsync(int id, CancellationToken ct = default)
        => _http.DeleteAsync($"/api/disciplinas/{id}", ct);
}

// Services/ObservacoesApi.cs
using System.Net.Http.Json;
using Importador.Front.Models;
using static Importador.Front.Services.ServiceCollectionExtensions;

namespace Importador.Front.Services;

public sealed class ObservacoesApi : IObservacoesApi
{
    private readonly HttpClient _http;
    public ObservacoesApi(IHttpClientFactory f) => _http = f.CreateClient("backend");

    // GET /api/alunos/{alunoId}/observacoes?importId=&page=&pageSize=&startUtc=&endUtc=  :contentReference[oaicite:8]{index=8}
    public Task<ObservacaoPage?> ListarAsync(
        int alunoId, Guid? importId = null, int page = 1, int pageSize = 50,
        DateTimeOffset? startUtc = null, DateTimeOffset? endUtc = null,
        CancellationToken ct = default)
    {
        var qp = new List<string> { $"page={page}", $"pageSize={pageSize}" };
        if (importId is not null) qp.Add($"importId={importId}");
        if (startUtc is not null) qp.Add($"startUtc={Uri.EscapeDataString(startUtc.Value.UtcDateTime.ToString("o"))}");
        if (endUtc is not null) qp.Add($"endUtc={Uri.EscapeDataString(endUtc.Value.UtcDateTime.ToString("o"))}");

        var url = $"/api/alunos/{alunoId}/observacoes" + (qp.Count > 0 ? "?" + string.Join("&", qp) : "");
        return _http.GetFromJsonAsync<ObservacaoPage>(url, Json, ct);
    }

    // POST /api/alunos/{alunoId}/observacoes  body: { texto, importId? }  :contentReference[oaicite:9]{index=9}
    public async Task<ObservacaoItem?> CriarAsync(int alunoId, ObservacaoCreate req, CancellationToken ct = default)
    {
        var resp = await _http.PostAsJsonAsync($"/api/alunos/{alunoId}/observacoes", req, Json, ct);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<ObservacaoItem>(Json, ct);
    }

    // GET /api/alunos/{alunoId}/observacoes/{obsId}  :contentReference[oaicite:10]{index=10}
    public Task<ObservacaoItem?> ObterAsync(int alunoId, int obsId, CancellationToken ct = default)
        => _http.GetFromJsonAsync<ObservacaoItem>($"/api/alunos/{alunoId}/observacoes/{obsId}", Json, ct);
}

// Services/ImportsApi.cs
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Importador.Front.Models;
using static Importador.Front.Services.ServiceCollectionExtensions;

namespace Importador.Front.Services;

public sealed class ImportsApi : IImportsApi
{
    private readonly HttpClient _http;

    public ImportsApi(IHttpClientFactory f) => _http = f.CreateClient("backend");

    public Task<HttpResponseMessage> UploadAsync(Stream file, string fileName, CancellationToken ct = default)
    {
        var content = new MultipartFormDataContent();
        var stream = new StreamContent(file);
        stream.Headers.ContentType = new MediaTypeHeaderValue(
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        content.Add(stream, "file", fileName);

        // POST /api/imports
        return _http.PostAsync("/api/imports", content, ct); // 202 Accepted esperado. :contentReference[oaicite:1]{index=1}
    }

    public Task<HttpResponseMessage> ProcessAsync(Guid importId, CancellationToken ct = default)
        => _http.PostAsync($"/api/imports/{importId}/process", null, ct); // 202. :contentReference[oaicite:2]{index=2}

    public Task<ImportStatus?> GetStatusAsync(Guid importId, CancellationToken ct = default)
        => _http.GetFromJsonAsync<ImportStatus>($"/api/imports/{importId}/status", Json, ct); // 200. :contentReference[oaicite:3]{index=3}

    public Task<List<AlunoListItem>?> GetAlunosFromImportAsync(Guid importId, CancellationToken ct = default)
        => _http.GetFromJsonAsync<List<AlunoListItem>>($"/api/imports/{importId}/alunos", Json, ct); // 200. :contentReference[oaicite:4]{index=4}

    // CSV matrícula/foto (export)
    public Task<HttpResponseMessage> ExportInfoCsvAsync(Guid importId, CancellationToken ct = default)
        => _http.GetAsync($"/api/imports/{importId}/alunos/export-info", ct); // content-type: text/csv. :contentReference[oaicite:5]{index=5}

    // CSV matrícula/foto (import)
    public async Task<(bool ok, string? message)> ImportInfoCsvAsync(Guid importId, Stream csv, string fileName, CancellationToken ct = default)
    {
        var content = new MultipartFormDataContent();
        var file = new StreamContent(csv);
        file.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
        content.Add(file, "file", fileName);

        // POST /api/imports/{id}/alunos/import-info
        var resp = await _http.PostAsync($"/api/imports/{importId}/alunos/import-info", content, ct); // :contentReference[oaicite:6]{index=6}
        var txt = await resp.Content.ReadAsStringAsync(ct);

        if (resp.IsSuccessStatusCode) return (true, txt);

        // tenta ProblemDetails/message
        try
        {
            using var doc = JsonDocument.Parse(txt);
            if (doc.RootElement.TryGetProperty("detail", out var d)) return (false, d.GetString());
            if (doc.RootElement.TryGetProperty("message", out var m)) return (false, m.GetString());
        }
        catch { /* ignore */ }

        return (false, $"{(int)resp.StatusCode} {resp.ReasonPhrase}");
    }


    public async Task<List<ImportListItem>> GetImportsAsync(CancellationToken ct = default)
    {
        // endpoint padrao da lista
        var list = await _http.GetFromJsonAsync<List<ImportListItem>>("/api/imports", Json, ct);
        return list ?? new();
    }

    public Task<ImportStatus?> GetImportStatusAsync(Guid importId, CancellationToken ct = default)
    {
        // reaproveita o método já existente
        return GetStatusAsync(importId, ct);
    }
}

using System.Net.Http.Headers;
using System.Net.Http.Json;
using Importador.Front.Models;

namespace Importador.Front.Services;

public sealed class ImportsApi : IImportsApi
{
    private readonly HttpClient _http;
    public ImportsApi(HttpClient http) => _http = http;

    public Task<List<ImportListItem>?> GetImportsAsync(CancellationToken ct = default)
        => _http.GetFromJsonAsync<List<ImportListItem>>("/api/imports", ct);

    public Task<ImportStatus?> GetImportStatusAsync(Guid id, CancellationToken ct = default)
        => _http.GetFromJsonAsync<ImportStatus>($"/api/imports/{id}/status", ct);

    public async Task<HttpResponseMessage> UploadAsync(Stream fileStream, string fileName, CancellationToken ct = default)
    {
        using var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        // "file" deve bater com o nome do parâmetro no backend
        content.Add(fileContent, "file", fileName);

        return await _http.PostAsync("/api/imports", content, ct);
    }

    public Task<List<AlunoListItem>?> GetAlunosFromImportAsync(Guid importId, CancellationToken ct = default)
        => _http.GetFromJsonAsync<List<AlunoListItem>>($"/api/imports/{importId}/alunos", ct);
}

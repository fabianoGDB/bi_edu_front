using System.Net.Http.Headers;
using System.Net.Http.Json;
using Importador.Front.Models;
using Microsoft.AspNetCore.Components;

namespace Importador.Front.Services;

public sealed class AlunosApi(HttpClient http, NavigationManager nav) : IAlunosApi
{
    private readonly HttpClient _http = http;
    private readonly NavigationManager _nav = nav;

    public async Task<AlunoDetalhe?> GetAlunoAsync(int alunoId, Guid? importId, CancellationToken ct = default)
    {
        var url = importId is null
            ? $"/api/alunos/{alunoId}"
            : $"/api/alunos/{alunoId}?importId={importId}";
        return await _http.GetFromJsonAsync<AlunoDetalhe>(url, ct);
    }

    public async Task<List<ObservacaoItem>> GetObservacoesAsync(int alunoId, Guid? importId, CancellationToken ct = default)
    {
        var url = importId is null
            ? $"/api/alunos/{alunoId}/conselho/observacoes"
            : $"/api/alunos/{alunoId}/conselho/observacoes?importId={importId}";
        return await _http.GetFromJsonAsync<List<ObservacaoItem>>(url, ct) ?? [];
    }

    public async Task<HttpResponseMessage> ImportObservacoesCsvAsync(int alunoId, Guid? importId, Stream csv, string fileName, CancellationToken ct = default)
    {
        using var form = new MultipartFormDataContent();
        var file = new StreamContent(csv);
        file.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
        form.Add(file, "file", fileName);

        var url = importId is null
            ? $"/api/alunos/{alunoId}/conselho/observacoes/import"
            : $"/api/alunos/{alunoId}/conselho/observacoes/import?importId={importId}";

        return await _http.PostAsync(url, form, ct);
    }

    public Task ExportObservacoesBaseCsvAsync(int alunoId, Guid? importId, CancellationToken ct = default)
    {
        var url = importId is null
            ? $"/api/alunos/{alunoId}/conselho/observacoes/base"
            : $"/api/alunos/{alunoId}/conselho/observacoes/base?importId={importId}";
        _nav.NavigateTo(url, forceLoad: true);
        return Task.CompletedTask;
    }
}

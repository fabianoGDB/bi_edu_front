using System.Net.Http.Headers;
using System.Net.Http.Json;
using Importador.Front.Models;
using Microsoft.AspNetCore.Components;

namespace Importador.Front.Services;

public sealed class ObservacoesApi(HttpClient http, NavigationManager nav) : IObservacoesApi
{
    private readonly HttpClient _http = http;
    private readonly NavigationManager _nav = nav;

    public async Task<ObservacaoPage> GetPageAsync(int alunoId, Guid? importId, int page = 1, int pageSize = 50, CancellationToken ct = default)
    {
        var qs = importId is null ? $"?page={page}&pageSize={pageSize}" : $"?importId={importId}&page={page}&pageSize={pageSize}";
        return await _http.GetFromJsonAsync<ObservacaoPage>($"/api/alunos/{alunoId}/conselho/observacoes{qs}", ct)
               ?? new ObservacaoPage { Items = new(), Total = 0, Page = page, PageSize = pageSize };
    }

    public Task<HttpResponseMessage> CreateAsync(int alunoId, Guid? importId, ObservacaoCreate dto, CancellationToken ct = default)
    {
        var url = importId is null
            ? $"/api/alunos/{alunoId}/conselho/observacoes"
            : $"/api/alunos/{alunoId}/conselho/observacoes?importId={importId}";
        return _http.PostAsJsonAsync(url, dto, ct);
    }

    public async Task<HttpResponseMessage> ImportCsvAsync(int alunoId, Guid? importId, Stream csv, string fileName, CancellationToken ct = default)
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

    public Task ExportBaseCsvAsync(int alunoId, Guid? importId, CancellationToken ct = default)
    {
        var url = importId is null
            ? $"/api/alunos/{alunoId}/conselho/observacoes/base"
            : $"/api/alunos/{alunoId}/conselho/observacoes/base?importId={importId}";
        _nav.NavigateTo(url, forceLoad: true);
        return Task.CompletedTask;
    }
}

using System.Net.Http.Json;
using Importador.Front.Models;

namespace Importador.Front.Services;

public sealed class AlunosApi : IAlunosApi
{
    private readonly HttpClient _http;
    public AlunosApi(HttpClient http) => _http = http;

    public Task<AlunoDetalhe?> GetAlunoAsync(int alunoId, Guid? importId = null, CancellationToken ct = default)
    {
        var url = importId is null
            ? $"/api/alunos/{alunoId}"
            : $"/api/students/{alunoId}?importId={importId}";
        return _http.GetFromJsonAsync<AlunoDetalhe>(url, ct);
    }
}

using Importador.Front.Models;

namespace Importador.Front.Services;

public interface IAlunosApi
{
    Task<AlunoDetalhe?> GetAlunoAsync(int alunoId, Guid? importId, CancellationToken ct = default);

    // Observações (CSV e listagem) — opcionais, mas usados no Conselho
    Task<List<ObservacaoItem>> GetObservacoesAsync(int alunoId, Guid? importId, CancellationToken ct = default);
    Task<HttpResponseMessage> ImportObservacoesCsvAsync(int alunoId, Guid? importId, Stream csv, string fileName, CancellationToken ct = default);
    Task ExportObservacoesBaseCsvAsync(int alunoId, Guid? importId, CancellationToken ct = default);
    Task<List<BimestreResumoDto>> GetResumoAsync(int alunoId, Guid? importId, CancellationToken ct = default);

}

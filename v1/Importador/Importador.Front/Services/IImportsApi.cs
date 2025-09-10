using System.Net.Http;
using Importador.Front.Models;

namespace Importador.Front.Services;

public interface IImportsApi
{
    Task<HttpResponseMessage> UploadAsync(Stream file, string fileName, CancellationToken ct = default);
    Task<HttpResponseMessage> ProcessAsync(Guid importId, CancellationToken ct = default);
    Task<ImportStatus?> GetStatusAsync(Guid importId, CancellationToken ct = default);
    Task<List<AlunoListItem>?> GetAlunosFromImportAsync(Guid importId, CancellationToken ct = default);
    Task<HttpResponseMessage> ExportInfoCsvAsync(Guid importId, CancellationToken ct = default);
    Task<(bool ok, string? message)> ImportInfoCsvAsync(Guid importId, Stream csv, string fileName, CancellationToken ct = default);

    // Aliases esperados por telas legado
    Task<List<ImportListItem>> GetImportsAsync(CancellationToken ct = default);
    Task<ImportStatus?> GetImportStatusAsync(Guid importId, CancellationToken ct = default);
}

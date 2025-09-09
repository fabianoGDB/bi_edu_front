// Services/IImportsApi.cs
using Importador.Front.Models;
using System.Net.Http;

namespace Importador.Front.Services;

public interface IImportsApi
{
    Task<HttpResponseMessage> UploadAsync(Stream file, string fileName, CancellationToken ct = default);
    Task<HttpResponseMessage> ProcessAsync(Guid importId, CancellationToken ct = default);
    Task<ImportStatus?> GetStatusAsync(Guid importId, CancellationToken ct = default);

    Task<List<AlunoListItem>?> GetAlunosFromImportAsync(Guid importId, CancellationToken ct = default);

    // CSV matr�cula/foto
    Task<HttpResponseMessage> ExportInfoCsvAsync(Guid importId, CancellationToken ct = default);
    Task<(bool ok, string? message)> ImportInfoCsvAsync(Guid importId, Stream csv, string fileName, CancellationToken ct = default);

    Task<List<ImportListItem>> GetImportsAsync(CancellationToken ct = default);
    Task<ImportStatus?> GetImportStatusAsync(Guid importId, CancellationToken ct = default);

}

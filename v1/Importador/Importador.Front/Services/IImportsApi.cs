using Importador.Front.Models;

namespace Importador.Front.Services;

public interface IImportsApi
{
    Task<List<ImportListItem>?> GetImportsAsync(CancellationToken ct = default);
    Task<ImportStatus?> GetImportStatusAsync(Guid id, CancellationToken ct = default);
    Task<HttpResponseMessage> UploadAsync(Stream fileStream, string fileName, CancellationToken ct = default);
    Task<List<AlunoListItem>?> GetAlunosFromImportAsync(Guid importId, CancellationToken ct = default);
}

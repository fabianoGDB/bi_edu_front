using System.Net.Http;
using Importador.Front.Models;

namespace Importador.Front.Services;

public interface IObservacoesApi
{
    Task<ObservacaoPage> GetPageAsync(int alunoId, Guid? importId, int page = 1, int pageSize = 50, CancellationToken ct = default);
    Task<HttpResponseMessage> CreateAsync(int alunoId, Guid? importId, ObservacaoCreate dto, CancellationToken ct = default);
    Task<HttpResponseMessage> ImportCsvAsync(int alunoId, Guid? importId, Stream csv, string fileName, CancellationToken ct = default);
    Task ExportBaseCsvAsync(int alunoId, Guid? importId, CancellationToken ct = default);
}

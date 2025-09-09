// Services/IObservacoesApi.cs
using Importador.Front.Models;

namespace Importador.Front.Services;

public interface IObservacoesApi
{
    Task<ObservacaoPage?> ListarAsync(
        int alunoId, Guid? importId = null, int page = 1, int pageSize = 50,
        DateTimeOffset? startUtc = null, DateTimeOffset? endUtc = null,
        CancellationToken ct = default);

    Task<ObservacaoItem?> CriarAsync(int alunoId, ObservacaoCreate req, CancellationToken ct = default);
    Task<ObservacaoItem?> ObterAsync(int alunoId, int obsId, CancellationToken ct = default);
}

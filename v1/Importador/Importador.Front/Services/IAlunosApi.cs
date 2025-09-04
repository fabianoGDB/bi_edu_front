using Importador.Front.Models;

namespace Importador.Front.Services;

public interface IAlunosApi
{
    Task<AlunoDetalhe?> GetAlunoAsync(int alunoId, Guid? importId = null, CancellationToken ct = default);
}

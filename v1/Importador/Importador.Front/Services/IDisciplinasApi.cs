using Importador.Front.Models;

namespace Importador.Front.Services;

public interface IDisciplinasApi
{
    Task<DisciplinaListPage?> GetPageAsync(int? areaId, string? q, int page, int pageSize, CancellationToken ct = default);
    Task<DisciplinaDto?> GetAsync(int id, CancellationToken ct = default);
    Task<DisciplinaDto?> CreateAsync(DisciplinaUpsert dto, CancellationToken ct = default);
    Task<DisciplinaDto?> UpdateAsync(int id, DisciplinaUpsert dto, CancellationToken ct = default);
    Task<HttpResponseMessage> DeleteAsync(int id, CancellationToken ct = default);
}

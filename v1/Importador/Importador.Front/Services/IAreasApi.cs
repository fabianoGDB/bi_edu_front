using Importador.Front.Models;

namespace Importador.Front.Services;

public interface IAreasApi
{
    Task<List<AreaListItem>?> GetAllAsync();
    Task<AreaDto?> GetAsync(int id, CancellationToken ct = default);
    Task<AreaDto?> CreateAsync(AreaUpsert dto, CancellationToken ct = default);
    Task<AreaDto?> UpdateAsync(int id, AreaUpsert dto, CancellationToken ct = default);
    Task<HttpResponseMessage> DeleteAsync(int id, CancellationToken ct = default);
}

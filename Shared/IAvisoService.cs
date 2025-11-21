using Entities;
using Entities.DTO;

namespace Shared
{
    public interface IAvisoService
    {
        Task<Aviso> GetByIdAsync(int id);
        Task<List<Aviso>> GetAtivosAsync();

        Task<Aviso> CreateAsync(AvisoDto dto);

        Task<Aviso> UpdateMensagemAsync(AvisoDto dto);

        Task<bool> DeleteAsync(int id);
    }
}

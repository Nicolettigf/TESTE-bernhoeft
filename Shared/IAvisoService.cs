using Entities;

namespace Shared
{
    public interface IAvisoService
    {
        Task<Aviso> GetByIdAsync(int id);
        Task<Aviso> CreateAsync(Aviso aviso);
        Task<Aviso> UpdateMensagemAsync(int id, string mensagem);
        Task<bool> DeleteAsync(int id);
        Task<List<Aviso>> GetAtivosAsync();
    }

}

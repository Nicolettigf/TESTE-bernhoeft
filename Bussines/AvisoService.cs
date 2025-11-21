using Entities;
using Shared;
using Microsoft.EntityFrameworkCore;

namespace Bussines
{
    public class AvisoService : IAvisoService
    {
        private readonly IAvisoRepository _repo;

        public AvisoService(IAvisoRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Aviso>> GetAtivosAsync()=> _repo.Query().Where(a => a.Ativo).ToListAsync();


        public async Task<Aviso> GetByIdAsync(int id)
        {
            return await _repo.Query().FirstOrDefaultAsync(a => a.Id == id && a.Ativo);
        }

        public async Task<Aviso> CreateAsync(Aviso aviso)
        {
            aviso.CriadoEm = DateTime.UtcNow;
            await _repo.AddAsync(aviso);
            return aviso;
        }

        public async Task<Aviso> UpdateMensagemAsync(int id, string mensagem)
        {
            var aviso = await GetByIdAsync(id);
            aviso.Mensagem = mensagem;
            aviso.EditadoEm = DateTime.UtcNow;
            await _repo.UpdateAsync(aviso);
            return aviso;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var aviso = await GetByIdAsync(id);
            aviso.Ativo = false;
            await _repo.UpdateAsync(aviso);
            return true;
        }
    }

}

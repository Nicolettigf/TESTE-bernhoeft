using Entities;
using Entities.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Bussines
{
    public class AvisoService : IAvisoService
    {
        private readonly IAvisoRepository _repo;
        private readonly CreateAvisoValidator _createValidator;
        private readonly UpdateAvisoValidator _updateValidator;
        private readonly GetAvisoValidator _getValidator;

        public AvisoService(
            IAvisoRepository repo,
            CreateAvisoValidator createValidator,
            UpdateAvisoValidator updateValidator,
            GetAvisoValidator getValidator)
        {
            _repo = repo;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _getValidator = getValidator;
        }

        // -------------------------------------------
        // GET ALL – somente ativos
        // -------------------------------------------
        public Task<List<Aviso>> GetAtivosAsync() =>
            _repo.Query()
                 .AsNoTracking()
                 .Where(a => a.Ativo)
                 .ToListAsync();


        // -------------------------------------------
        // GET BY ID com validação
        // -------------------------------------------
        public async Task<Aviso?> GetByIdAsync(int id)
        {
            await _getValidator.ValidateAndThrowAsync(new GetAvisoDto { Id = id });

            return await _repo
                .Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id && a.Ativo);
        }


        // -------------------------------------------
        // CREATE
        // -------------------------------------------
        public async Task<Aviso> CreateAsync(AvisoDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);

            var aviso = new Aviso
            {
                Titulo = dto.Titulo.Trim(),
                Mensagem = dto.Mensagem.Trim(),
                CriadoEm = DateTime.UtcNow,
                Ativo = true
            };

            await _repo.AddAsync(aviso);
            return aviso;
        }


        // -------------------------------------------
        // UPDATE (somente mensagem)
        // -------------------------------------------
        public async Task<Aviso> UpdateMensagemAsync(AvisoDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);

            // Reaproveita validação do GET
            var aviso = await _repo.Query().FirstOrDefaultAsync(a => a.Id == dto.Id && a.Ativo);

            if (aviso == null)
                throw new KeyNotFoundException("Aviso não encontrado.");

            aviso.EditadoEm = DateTime.UtcNow;

            await _repo.UpdateAsync(aviso);

            return aviso;
        }


        // -------------------------------------------
        // DELETE (soft delete)
        // -------------------------------------------
        public async Task<bool> DeleteAsync(int id)
        {
            await _getValidator.ValidateAndThrowAsync(new GetAvisoDto { Id = id });

            // Aqui precisa rastrear a entidade para update
            var aviso = await _repo.Query().FirstOrDefaultAsync(a => a.Id == id && a.Ativo);

            if (aviso == null)
                return false;

            aviso.Ativo = false;
            await _repo.UpdateAsync(aviso);

            return true;
        }
    }
}

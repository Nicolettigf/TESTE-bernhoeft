using DataAcess;
using Entities;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace DataAccess
{
    public class AvisoRepository : IAvisoRepository
    {
        private readonly AppDbContext _context;

        public AvisoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Aviso> Query()
        {
            return _context.Avisos.AsQueryable();
        }

        public async Task AddAsync(Aviso aviso)
        {
            _context.Avisos.Add(aviso);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Aviso aviso)
        {
            _context.Avisos.Update(aviso);
            await _context.SaveChangesAsync();
        }
    }
}

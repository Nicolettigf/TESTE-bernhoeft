using Entities;

namespace Shared
{
    public interface IAvisoRepository
    {
        IQueryable<Aviso> Query();
        Task AddAsync(Aviso aviso);
        Task UpdateAsync(Aviso aviso);
    }
}

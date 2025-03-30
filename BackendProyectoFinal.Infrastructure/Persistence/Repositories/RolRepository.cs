using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;

namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class RolRepository : GenericRepository<Rol>, IRolRepository
    {
        public RolRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implementaciones específicas de IRolRepository si las hay
    }
}

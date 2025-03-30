using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;

namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implementaciones específicas de ICategoriaRepository si las hay
    }
}

using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class ImagenRepository : GenericRepository<Imagen>, IImagenRepository
    {
        private readonly ApplicationDbContext _context;

        public ImagenRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task DeleteAsync(Imagen imagen)
        {
            _context.Imagenes.Remove(imagen);
            await _context.SaveChangesAsync();
        }
    }
}
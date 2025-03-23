using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class ImagenRepository : GenericRepository<Imagen>, IImagenRepository
    {
        public ImagenRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

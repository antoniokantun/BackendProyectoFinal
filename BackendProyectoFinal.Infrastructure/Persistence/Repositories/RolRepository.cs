using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using BackendProyectoFinal.Infrastructure.RepositoriesBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

﻿using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IPerfilRepository : IGenericRepository<Perfil>
    {
        // Métodos específicos para Perfil si se necesitan
        Task<IEnumerable<Perfil>> GetPerfilesByUsuarioIdAsync(int usuarioId);
    }
}

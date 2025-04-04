﻿using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IUsuarioReporteRepository : IGenericRepository<UsuarioReporte>
    {
        Task<IEnumerable<UsuarioReporte>> GetAllUsuarioReportesWithDetailsAsync();
    }
}

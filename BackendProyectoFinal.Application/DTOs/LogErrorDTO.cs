﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class LogErrorDTO
    {
        public int IdLogError { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public DateTime FechaOcurrencia { get; set; }
        public string Origen { get; set; } = string.Empty;
        public string Severidad { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        public int? UsuarioId { get; set; }
    }
}

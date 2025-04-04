﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        
        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;
        
        public string CorreoElectronico { get; set; } = string.Empty;
        
        public string? Telefono { get; set; }
        
        public string Contrasenia { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }
        public bool Baneado { get; set; } = false;

        public bool Reportado { get; set; } = false;


        public int RolId { get; set; }

        public string? NombreRol { get; set; }
    }

    // DTO para creación y actualización de usuario (sin exponer contraseña en respuestas)
    public class UsuarioCreacionDTO
    {
        
        public string Nombre { get; set; } = string.Empty;
        
        public string Apellido { get; set; } = string.Empty;
        
        public string CorreoElectronico { get; set; } = string.Empty;
       
        public string? Telefono { get; set; }
        
        public string Contrasenia { get; set; } = string.Empty;
        public bool Baneado { get; set; } = false;
        public bool Reportado { get; set; } = false;


        [Required(ErrorMessage = "El rol es requerido")]
        public int RolId { get; set; }
    }

    // DTO para actualización de usuario (contraseña opcional)
    public class UsuarioActualizacionDTO
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "El teléfono no puede superar los 50 caracteres")]
        public string? Telefono { get; set; }

        // Contraseña opcional para actualización
        public string? Contrasenia { get; set; }
        public bool Baneado { get; set; } = false;
        public bool Reportado { get; set; } = false;


        [Required(ErrorMessage = "El rol es requerido")]
        public int RolId { get; set; }
    }

    // DTO para respuestas (sin exponer la contraseña)
    public class UsuarioRespuestaDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Baneado { get; set; } = false;
        public bool Reportado { get; set; }

        public int RolId { get; set; }
        public string NombreRol { get; set; } = string.Empty;
    }

    public class UpdateUserBanDTO
    {
        public int IdUsuario { get; set; }

        public bool Baneado { get; set; }
    }

    public class UpdateUserReportDTO
    {
        public int IdUsuario { get; set; }
        public bool Reportado { get; set; }
    }
}

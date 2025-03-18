using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Contrasenia { get; set; } = string.Empty;
    }

    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public UsuarioRespuestaDTO Usuario { get; set; } = null!;
    }
}

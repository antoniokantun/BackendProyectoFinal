using BackendProyectoFinal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioRespuestaDTO>> GetAllAsync();
        Task<UsuarioRespuestaDTO> GetByIdAsync(int id);
        Task<UsuarioRespuestaDTO> CreateAsync(UsuarioCreacionDTO usuarioDto);
        Task UpdateAsync(UsuarioActualizacionDTO usuarioDto);
        Task DeleteAsync(int id);
        Task<UsuarioRespuestaDTO> GetByEmailAsync(string email);
        Task<bool> VerifyCredentialsAsync(string email, string password);
    }
}

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
        Task UpdateAsync(int id, UsuarioActualizacionDTO usuarioDto);
        Task DeleteAsync(int id);
        Task<UsuarioRespuestaDTO> GetByEmailAsync(string email);
        Task<bool> VerifyCredentialsAsync(string email, string password);

        //Interfaz para actualizar campo de baneado en tabla Usuarios.
        Task UpdateBanStatusAsync(UpdateUserBanDTO updateBanDTO);

    }
}

using BackendProyectoFinal.Application.DTOs;


namespace BackendProyectoFinal.Application.Interfaces
{
    public interface IIntercambioService
    {
        Task<IEnumerable<IntercambioDTO>> GetAllAsync();
        Task<IntercambioDTO> GetByIdAsync(int id);
        Task<IntercambioDTO> CreateAsync(CreateIntercambioDTO intercambioDto);
        Task UpdateAsync(int id, UpdateIntercambioDTO intercambioDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<IntercambioDTO>> GetByUsuarioSolicitanteIdAsync(int usuarioSolicitanteId);
        Task<IEnumerable<IntercambioDTO>> GetByUsuarioOfertanteIdAsync(int usuarioOfertanteId);
        Task<IEnumerable<IntercambioDTO>> GetByProductoIdAsync(int productoId);
    }
}

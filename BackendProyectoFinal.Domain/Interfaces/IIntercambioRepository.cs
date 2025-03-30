using BackendProyectoFinal.Domain.Entities;


namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IIntercambioRepository : IGenericRepository<Intercambio>
    {
        // Métodos específicos para Intercambio si se necesitan
        Task<IEnumerable<Intercambio>> GetByUsuarioSolicitanteIdAsync(int usuarioSolicitanteId);
        Task<IEnumerable<Intercambio>> GetByUsuarioOfertanteIdAsync(int usuarioOfertanteId);
        Task<IEnumerable<Intercambio>> GetByProductoIdAsync(int productoId);
    }
}

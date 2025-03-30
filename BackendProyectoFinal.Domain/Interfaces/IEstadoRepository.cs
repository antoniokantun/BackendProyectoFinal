using BackendProyectoFinal.Domain.Entities;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IEstadoRepository : IGenericRepository<Estado>
    {
        // Métodos específicos para Estado si se necesitan
        Task<Estado?> GetByNombreAsync(string nombre);
    }
}

using BackendProyectoFinal.Domain.Entities;


namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IProductoReporteRepository : IGenericRepository<ProductoReporte>
    {
        Task<IEnumerable<ProductoReporte>> GetAllProductoReportesWithDetailsAsync();
    }
}

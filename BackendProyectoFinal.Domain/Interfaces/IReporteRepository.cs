using BackendProyectoFinal.Domain.Entities;


namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IReporteRepository : IGenericRepository<Reporte>
    {
        Task<IEnumerable<Reporte>> GetAllReportesWithDetailsAsync();
        Task<Reporte?> GetReporteWithDetailsAsync(int id);
    }
}

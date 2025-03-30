using BackendProyectoFinal.Application.DTOs;


namespace BackendProyectoFinal.Application.Interfaces
{
    public interface IReporteService
    {
        Task<IEnumerable<ReporteDTO>> GetAllReportesAsync();
        Task<IEnumerable<ReporteUsuarioDTO>> GetAllReportesUsuarioAsync();
        Task<IEnumerable<ReporteProductoDTO>> GetAllReportesProductoAsync();
        Task<ReporteDTO> GetByIdAsync(int id);
        Task<ReporteUsuarioDTO> ReportarUsuarioAsync(ReporteUsuarioCreateForm reporteForm);
        Task<ReporteProductoDTO> ReportarProductoAsync(ReporteProductoCreateForm reporteForm);
    }
}

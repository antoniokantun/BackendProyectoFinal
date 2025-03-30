using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;


namespace BackendProyectoFinal.Application.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _reporteRepository;
        private readonly IUsuarioReporteRepository _usuarioReporteRepository;
        private readonly IProductoReporteRepository _productoReporteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProductoRepository _productoRepository;

        public ReporteService(
            IReporteRepository reporteRepository,
            IUsuarioReporteRepository usuarioReporteRepository,
            IProductoReporteRepository productoReporteRepository,
            IUsuarioRepository usuarioRepository,
            IProductoRepository productoRepository)
        {
            _reporteRepository = reporteRepository;
            _usuarioReporteRepository = usuarioReporteRepository;
            _productoReporteRepository = productoReporteRepository;
            _usuarioRepository = usuarioRepository;
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<ReporteDTO>> GetAllReportesAsync()
        {
            var reportes = await _reporteRepository.GetAllAsync();
            return reportes.Select(r => new ReporteDTO
            {
                IdReporte = r.IdReporte,
                MotivoReporte = r.MotivoReporte,
                FechaReporte = r.FechaReporte
            });
        }

        public async Task<IEnumerable<ReporteUsuarioDTO>> GetAllReportesUsuarioAsync()
        {
            var usuarioReportes = await _usuarioReporteRepository.GetAllUsuarioReportesWithDetailsAsync();
            return usuarioReportes.Select(ur => new ReporteUsuarioDTO
            {
                IdReporte = ur.ReporteId,
                MotivoReporte = ur.Reporte?.MotivoReporte ?? string.Empty,
                FechaReporte = ur.Reporte?.FechaReporte ?? DateTime.UtcNow,
                UsuarioId = ur.UsuarioId,
                NombreUsuario = ur.Usuario?.Nombre // Asumiendo que la entidad Usuario tiene una propiedad Nombre
            });
        }

        public async Task<IEnumerable<ReporteProductoDTO>> GetAllReportesProductoAsync()
        {
            var productoReportes = await _productoReporteRepository.GetAllProductoReportesWithDetailsAsync();
            return productoReportes.Select(pr => new ReporteProductoDTO
            {
                IdReporte = pr.ReporteId,
                MotivoReporte = pr.Reporte?.MotivoReporte ?? string.Empty,
                FechaReporte = pr.Reporte?.FechaReporte ?? DateTime.UtcNow,
                ProductoId = pr.ProductoId,
                NombreProducto = pr.Producto?.Nombre // Asumiendo que la entidad Producto tiene una propiedad Nombre
            });
        }

        public async Task<ReporteDTO> GetByIdAsync(int id)
        {
            var reporte = await _reporteRepository.GetByIdAsync(id);

            if (reporte == null)
                throw new KeyNotFoundException($"Reporte con ID {id} no encontrado.");

            return new ReporteDTO
            {
                IdReporte = reporte.IdReporte,
                MotivoReporte = reporte.MotivoReporte,
                FechaReporte = reporte.FechaReporte
            };
        }

        public async Task<ReporteUsuarioDTO> ReportarUsuarioAsync(ReporteUsuarioCreateForm reporteForm)
        {
            // Verificar si el usuario existe
            var usuario = await _usuarioRepository.GetByIdAsync(reporteForm.UsuarioId);
            if (usuario == null)
                throw new KeyNotFoundException($"Usuario con ID {reporteForm.UsuarioId} no encontrado.");

            // Crear el reporte
            var reporte = new Reporte
            {
                MotivoReporte = reporteForm.MotivoReporte,
                FechaReporte = DateTime.UtcNow
            };

            var createdReporte = await _reporteRepository.AddAsync(reporte);

            // Crear la relación entre el usuario y el reporte
            var usuarioReporte = new UsuarioReporte
            {
                UsuarioId = reporteForm.UsuarioId,
                ReporteId = createdReporte.IdReporte
            };

            await _usuarioReporteRepository.AddAsync(usuarioReporte);

            return new ReporteUsuarioDTO
            {
                IdReporte = createdReporte.IdReporte,
                MotivoReporte = createdReporte.MotivoReporte,
                FechaReporte = createdReporte.FechaReporte,
                UsuarioId = reporteForm.UsuarioId,
                NombreUsuario = usuario.Nombre // Asumiendo que la entidad Usuario tiene una propiedad Nombre
            };
        }

        public async Task<ReporteProductoDTO> ReportarProductoAsync(ReporteProductoCreateForm reporteForm)
        {
            // Verificar si el producto existe
            var producto = await _productoRepository.GetByIdAsync(reporteForm.ProductoId);
            if (producto == null)
                throw new KeyNotFoundException($"Producto con ID {reporteForm.ProductoId} no encontrado.");

            // Crear el reporte
            var reporte = new Reporte
            {
                MotivoReporte = reporteForm.MotivoReporte,
                FechaReporte = DateTime.UtcNow
            };

            var createdReporte = await _reporteRepository.AddAsync(reporte);

            // Crear la relación entre el producto y el reporte
            var productoReporte = new ProductoReporte
            {
                ProductoId = reporteForm.ProductoId,
                ReporteId = createdReporte.IdReporte
            };

            await _productoReporteRepository.AddAsync(productoReporte);

            return new ReporteProductoDTO
            {
                IdReporte = createdReporte.IdReporte,
                MotivoReporte = createdReporte.MotivoReporte,
                FechaReporte = createdReporte.FechaReporte,
                ProductoId = reporteForm.ProductoId,
                NombreProducto = producto.Nombre // Asumiendo que la entidad Producto tiene una propiedad Nombre
            };
        }
    }
}

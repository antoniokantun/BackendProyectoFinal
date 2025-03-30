

namespace BackendProyectoFinal.Application.DTOs
{
    public class ReporteDTO
    {
        public int IdReporte { get; set; }
        public string MotivoReporte { get; set; } = string.Empty;
        public DateTime FechaReporte { get; set; }
    }

    public class ReporteUsuarioDTO
    {
        public int IdReporte { get; set; }
        public string MotivoReporte { get; set; } = string.Empty;
        public DateTime FechaReporte { get; set; }
        public int UsuarioId { get; set; }
        public string? NombreUsuario { get; set; }
    }

    public class ReporteProductoDTO
    {
        public int IdReporte { get; set; }
        public string MotivoReporte { get; set; } = string.Empty;
        public DateTime FechaReporte { get; set; }
        public int ProductoId { get; set; }
        public string? NombreProducto { get; set; }
    }

    public class ReporteUsuarioCreateForm
    {
        public int UsuarioId { get; set; }
        public string MotivoReporte { get; set; } = string.Empty;
    }

    public class ReporteProductoCreateForm
    {
        public int ProductoId { get; set; }
        public string MotivoReporte { get; set; } = string.Empty;
    }
}



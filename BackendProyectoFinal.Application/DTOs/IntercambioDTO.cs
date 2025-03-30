
namespace BackendProyectoFinal.Application.DTOs
{
    public class IntercambioDTO
    {
        public int IdIntercambio { get; set; }
        public int UsuarioSolicitanteId { get; set; }
        public int UsuarioOfertanteId { get; set; }
        public int ProductoId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int? EstadoId { get; set; }
    }

    public class CreateIntercambioDTO
    {
        public int UsuarioSolicitanteId { get; set; }
        public int UsuarioOfertanteId { get; set; }
        public int ProductoId { get; set; }
        public int? EstadoId { get; set; }
    }

    public class UpdateIntercambioDTO
    {
        public int UsuarioSolicitanteId { get; set; }
        public int UsuarioOfertanteId { get; set; }
        public int ProductoId { get; set; }
        public int? EstadoId { get; set; }
    }
}

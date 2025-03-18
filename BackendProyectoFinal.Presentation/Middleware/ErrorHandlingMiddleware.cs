using BackendProyectoFinal.Domain.Interfaces.IServices;
using System.Net;
using System.Text.Json;

namespace BackendProyectoFinal.Presentation.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IErrorHandlingService errorHandlingService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, errorHandlingService);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, IErrorHandlingService errorHandlingService)
        {
            // Registrar el error
            await errorHandlingService.LogErrorAsync(
                exception,
                $"{context.Request.Path} - {context.Request.Method}",
                "Error",
                null); // Aquí podrías obtener el ID del usuario si está autenticado

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            var response = new
            {
                error = exception.Message,
                statusCode = context.Response.StatusCode
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}

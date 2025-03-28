using BackendProyectoFinal.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Infrastructure.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _uploadsFolderPath;
        private readonly string _imagesUrlPrefix = "/images"; // Ruta virtual configurada

        public LocalFileStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _uploadsFolderPath = Path.Combine(_env.ContentRootPath, "..", "uploads"); // Ruta a la carpeta uploads en la raíz
            EnsureUploadsFolderExists();
        }

        private void EnsureUploadsFolderExists()
        {
            if (!Directory.Exists(_uploadsFolderPath))
            {
                Directory.CreateDirectory(_uploadsFolderPath);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file, string containerName)
        {
            if (file == null || file.Length == 0)
            {
                return null; // O manejar el error como prefieras
            }

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var folderPath = Path.Combine(_uploadsFolderPath, containerName);

            // Asegurar que la subcarpeta exista
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, fileName);

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                await File.WriteAllBytesAsync(filePath, ms.ToArray());
            }

            // Construir la URL completa de acceso
            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var imageUrl = $"{baseUrl}{_imagesUrlPrefix}/{containerName}/{fileName}";
            return imageUrl;
        }

        public async Task<bool> DeleteFileAsync(string fileUrl, string subFolderName)
        {
            if (string.IsNullOrEmpty(fileUrl))
            {
                return false;
            }

            try
            {
                // Obtener el nombre del archivo de la URL
                string fileName = Path.GetFileName(fileUrl);

                // Si la URL tiene el formato completo (con el prefijo)
                if (fileUrl.StartsWith(_imagesUrlPrefix))
                {
                    // Quitar el prefijo y el subfolder de la URL para obtener solo el nombre del archivo
                    string relativePath = fileUrl.Replace(_imagesUrlPrefix, string.Empty).TrimStart('/');
                    fileName = Path.GetFileName(relativePath);
                }

                string filePath = Path.Combine(_uploadsFolderPath, subFolderName, fileName);

                // Verificar si el archivo existe antes de eliminarlo
                if (File.Exists(filePath))
                {
                    // Eliminar el archivo físicamente
                    File.Delete(filePath);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                // Manejar la excepción según tus necesidades
                return false;
            }

            // El método es async para mantener consistencia con otros métodos,
            // pero realmente no necesita ser async ya que File.Delete es sincrónico.
            // Añadimos esta línea para satisfacer el compilador.
            return await Task.FromResult(false);
        }
    }
}
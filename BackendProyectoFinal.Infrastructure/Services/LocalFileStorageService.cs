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

        public Task DeleteFileAsync(string fileRoute, string containerName)
        {
            if (string.IsNullOrEmpty(fileRoute))
            {
                return Task.CompletedTask;
            }

            // La ruta que guardamos en la base de datos es ahora completa, así que podemos usarla directamente
            var fileName = Path.GetFileName(fileRoute);
            var fileDirectory = Path.Combine(_uploadsFolderPath, containerName, fileName);

            if (File.Exists(fileDirectory))
            {
                File.Delete(fileDirectory);
            }

            return Task.CompletedTask;
        }
    }
}
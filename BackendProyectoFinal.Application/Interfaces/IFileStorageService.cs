using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string subFolderName);
}

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly string _uploadsFolderPath;
    private readonly string _imagesUrlPrefix = "/images"; // Coincide con RequestPath en UseStaticFiles

    public FileStorageService(IWebHostEnvironment environment)
    {
        _environment = environment;
        _uploadsFolderPath = Path.Combine(_environment.ContentRootPath, "..", "uploads"); // Ruta a la carpeta uploads en la raíz
        EnsureUploadsFolderExists();
    }

    private void EnsureUploadsFolderExists()
    {
        if (!Directory.Exists(_uploadsFolderPath))
        {
            Directory.CreateDirectory(_uploadsFolderPath);
        }
    }

    public async Task<string> SaveFileAsync(IFormFile file, string subFolderName)
    {
        if (file == null || file.Length == 0)
        {
            return null; // O manejar el error como prefieras
        }

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var folderPath = Path.Combine(_uploadsFolderPath, subFolderName);

        // Asegurar que la subcarpeta exista
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var filePath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Construir la URL de acceso utilizando la ruta virtual configurada
        var imageUrl = $"{_imagesUrlPrefix}/{subFolderName}/{fileName}";
        return imageUrl;
    }
}
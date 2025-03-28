using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string subFolderName);
    Task<bool> DeleteFileAsync(string fileUrl, string subFolderName);
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BackendProyectoFinal.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Obtén la ruta del archivo appsettings.json en la carpeta de Presentación
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../BackendProyectoFinal.Presentation"));

            // Configura la conexión a la base de datos
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) // Establece la ruta base
                .AddJsonFile("appsettings.json") // Lee el archivo appsettings.json
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Ajusta según tu proveedor de base de datos
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); // Para MySQL
            // builder.UseSqlServer(connectionString); // Para SQL Server
            // builder.UseNpgsql(connectionString); // Para PostgreSQL
            // builder.UseSqlite(connectionString); // Para SQLite

            return new ApplicationDbContext(builder.Options);
        }
    }
}
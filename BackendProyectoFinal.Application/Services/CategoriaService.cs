using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;


namespace BackendProyectoFinal.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IFileStorageService _fileStorageService;

        public CategoriaService(
            ICategoriaRepository categoriaRepository,
            IFileStorageService fileStorageService)
        {
            _categoriaRepository = categoriaRepository;
            _fileStorageService = fileStorageService;
        }

        // Métodos existentes...
        public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return categorias.Select(c => new CategoriaDTO
            {
                IdCategoria = c.IdCategoria,
                Nombre = c.Nombre,
                ImagenCategoria = c.ImagenCategoria
            });
        }

        public async Task<CategoriaDTO> GetByIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);

            if (categoria == null)
                throw new KeyNotFoundException($"Categoría con ID {id} no encontrada.");

            return new CategoriaDTO
            {
                IdCategoria = categoria.IdCategoria,
                Nombre = categoria.Nombre,
                ImagenCategoria = categoria.ImagenCategoria
            };
        }

        public async Task<CategoriaDTO> CreateAsync(CategoriaDTO categoriaDto)
        {
            var categoria = new Categoria
            {
                Nombre = categoriaDto.Nombre,
                ImagenCategoria = categoriaDto.ImagenCategoria
            };

            var createdCategoria = await _categoriaRepository.AddAsync(categoria);

            return new CategoriaDTO
            {
                IdCategoria = createdCategoria.IdCategoria,
                Nombre = createdCategoria.Nombre,
                ImagenCategoria = createdCategoria.ImagenCategoria

            };
        }

        public async Task UpdateAsync(CategoriaDTO categoriaDto)
        {
            var existingCategoria = await _categoriaRepository.GetByIdAsync(categoriaDto.IdCategoria);

            if (existingCategoria == null)
                throw new KeyNotFoundException($"Categoría con ID {categoriaDto.IdCategoria} no encontrada.");

            existingCategoria.Nombre = categoriaDto.Nombre;
            existingCategoria.ImagenCategoria = categoriaDto.ImagenCategoria;

            await _categoriaRepository.UpdateAsync(existingCategoria);
        }

        public async Task DeleteAsync(int id)
        {
            var existingCategoria = await _categoriaRepository.GetByIdAsync(id);

            if (existingCategoria == null)
                throw new KeyNotFoundException($"Categoría con ID {id} no encontrada.");

            // Intentar eliminar la imagen física si existe
            if (!string.IsNullOrEmpty(existingCategoria.ImagenCategoria))
            {
                // Simplemente continuamos con la ejecución aunque no se pueda eliminar el archivo físico
                // Si se implementa DeleteFileAsync, podrá eliminar el archivo
                try
                {
                    await _fileStorageService.DeleteFileAsync(existingCategoria.ImagenCategoria, "categorias");
                }
                catch
                {
                    // Ignorar errores al eliminar el archivo
                }
            }

            await _categoriaRepository.DeleteAsync(id);
        }

        // Nuevos métodos implementados
        public async Task<CategoriaDTO> CreateWithImageAsync(CategoriaCreateForm categoriaForm)
        {
            // Guarda la imagen y obtiene la URL
            string imagenUrl = await _fileStorageService.SaveFileAsync(categoriaForm.Imagen, "categorias");

            // Crea la entidad categoría
            var categoria = new Categoria
            {
                Nombre = categoriaForm.Nombre,
                ImagenCategoria = imagenUrl
            };

            // Guarda la categoría en la base de datos
            var createdCategoria = await _categoriaRepository.AddAsync(categoria);

            // Retorna el DTO
            return new CategoriaDTO
            {
                IdCategoria = createdCategoria.IdCategoria,
                Nombre = createdCategoria.Nombre,
                ImagenCategoria = createdCategoria.ImagenCategoria
            };
        }

        public async Task<CategoriaDTO> UpdateWithImageAsync(CategoriaEditForm categoriaForm)
        {
            // Obtiene la categoría existente
            var existingCategoria = await _categoriaRepository.GetByIdAsync(categoriaForm.IdCategoria);

            if (existingCategoria == null)
                throw new KeyNotFoundException($"Categoría con ID {categoriaForm.IdCategoria} no encontrada.");

            // Actualiza el nombre
            existingCategoria.Nombre = categoriaForm.Nombre;

            // Si hay una nueva imagen, la guarda y actualiza la URL
            if (categoriaForm.NuevaImagen != null)
            {
                // Intentar eliminar la imagen anterior si existe
                if (!string.IsNullOrEmpty(existingCategoria.ImagenCategoria))
                {
                    try
                    {
                        await _fileStorageService.DeleteFileAsync(existingCategoria.ImagenCategoria, "categorias");
                    }
                    catch
                    {
                        // Ignorar errores al eliminar el archivo
                    }
                }

                // Guarda la nueva imagen
                string nuevaImagenUrl = await _fileStorageService.SaveFileAsync(categoriaForm.NuevaImagen, "categorias");
                existingCategoria.ImagenCategoria = nuevaImagenUrl;
            }

            // Actualiza la categoría en la base de datos
            await _categoriaRepository.UpdateAsync(existingCategoria);

            // Retorna el DTO actualizado
            return new CategoriaDTO
            {
                IdCategoria = existingCategoria.IdCategoria,
                Nombre = existingCategoria.Nombre,
                ImagenCategoria = existingCategoria.ImagenCategoria
            };
        }
    }
}

using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return categorias.Select(c => new CategoriaDTO
            {
                IdCategoria = c.IdCategoria,
                Nombre = c.Nombre
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
                Nombre = categoria.Nombre
            };
        }

        public async Task<CategoriaDTO> CreateAsync(CategoriaDTO categoriaDto)
        {
            var categoria = new Categoria
            {
                Nombre = categoriaDto.Nombre
            };

            var createdCategoria = await _categoriaRepository.AddAsync(categoria);

            return new CategoriaDTO
            {
                IdCategoria = createdCategoria.IdCategoria,
                Nombre = createdCategoria.Nombre
            };
        }

        public async Task UpdateAsync(CategoriaDTO categoriaDto)
        {
            var existingCategoria = await _categoriaRepository.GetByIdAsync(categoriaDto.IdCategoria);

            if (existingCategoria == null)
                throw new KeyNotFoundException($"Categoría con ID {categoriaDto.IdCategoria} no encontrada.");

            existingCategoria.Nombre = categoriaDto.Nombre;

            await _categoriaRepository.UpdateAsync(existingCategoria);
        }

        public async Task DeleteAsync(int id)
        {
            var existingCategoria = await _categoriaRepository.GetByIdAsync(id);

            if (existingCategoria == null)
                throw new KeyNotFoundException($"Categoría con ID {id} no encontrada.");

            await _categoriaRepository.DeleteAsync(id);
        }
    }
}

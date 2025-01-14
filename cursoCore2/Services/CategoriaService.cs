using AutoMapper;
using cursoCore2API.DTOs;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;
using cursoCore2API.Services.IServices;
using Microsoft.AspNetCore.Identity;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace cursoCore2API.Services
{
    public class CategoriaService : ICategoriaService 
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public IReadOnlyList<string> Errors => throw new NotImplementedException();

        public CategoriaService(ICategoriaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDto>> GetCategoriasAsync()
        {
            var listaCategorias = await _repository.GetCategoriasAsync();
            return listaCategorias.Select(categoria => _mapper.Map<CategoriaDto>(categoria)).ToList();

        }


        public async Task<CategoriaDto> GetCategoriaByIdAsync(int id)
        {
            var categoria = await _repository.GetCategoriaByid(id);

            if(categoria == null)
            {
                return default; 
            }

            var categoriaDto = _mapper.Map<CategoriaDto>(categoria);

            return categoriaDto;
        }

        public async Task<bool> ExisteCategoriaAsync(int categoriaId)
        {
            var categoria = await _repository.GetByIdAsync(categoriaId);
            return categoria != null;
             
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = await _repository.GetByIdAsync(id);

            if (categoria == null)
            {
                return false; 
            }

            await _repository.RemoveAsync(categoria); 
            return true;
        }
        
        public Task<Categoria> AddAsync(CategoriaInsertDto createDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Categoria> UpdateAsync(int id, CategoriaUpdateDto updateDto)
        {
            try
            {
                var categoriaExistente = await _repository.GetByIdAsync(id);
                if (categoriaExistente == null)
                {
                    return null; 
                }

                _mapper.Map(updateDto, categoriaExistente);

                var result = await _repository.UpdateAsync(categoriaExistente);
                if (!result)
                {
                    return null; 
                }

                await _repository.SaveChangesAsync();

                return categoriaExistente; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar: {ex.Message}");
                return null;
            }
        }



        public async Task<Categoria> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return null; 
            }

            var dto = _mapper.Map<Categoria>(entity); 

            return dto;
        }


        public Task<IEnumerable<Categoria>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Validate(CategoriaInsertDto dto)
        {
            throw new NotImplementedException();
        }

        public bool Validate(CategoriaUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        
    }
}

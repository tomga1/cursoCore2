using cursoCore2API.DTOs;
using cursoCore2API.Models;

namespace cursoCore2API.Services.IServices
{
    public interface ICategoriaService : IServiceBase<Categoria, CategoriaInsertDto, CategoriaUpdateDto>
    {
        Task<List<CategoriaDto>> GetCategoriasAsync();
        Task<CategoriaDto> GetCategoriaByIdAsync(int id);
        Task<bool> ExisteCategoriaAsync(int id);
        Task<Categoria> UpdateAsync(int id, CategoriaUpdateDto updateDto); 


    }
}

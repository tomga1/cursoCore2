using cursoCore2API.DTOs;
using cursoCore2API.Models;

namespace cursoCore2API.Services.IServices
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> GetCategoriasAsync();
        Task<CategoriaDto> GetCategoriaByIdAsync(int id);

    }
}

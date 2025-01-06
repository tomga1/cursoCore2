using AutoMapper;
using cursoCore2API.DTOs;
using cursoCore2API.Repository.IRepository;
using cursoCore2API.Services.IServices;

namespace cursoCore2API.Services
{
    public class CategoriaService : ICategoriaService 
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

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





    }
}

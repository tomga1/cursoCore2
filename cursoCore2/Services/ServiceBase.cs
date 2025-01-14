using AutoMapper;
using cursoCore2API.DTOs;
using cursoCore2API.Repository.IRepository;
using Microsoft.AspNetCore.Components.Web;

namespace cursoCore2API.Services.IServices
{
    public class ServiceBase<T, TCreateDto, TUpdateDto> : IServiceBase<T, TCreateDto, TUpdateDto> where T : class
    {
        protected readonly IRepositoryBase<T> _repository;
        protected readonly IMapper _mapper;
        protected readonly List<string> _errors = new();

        public IReadOnlyList<string> Errors => _errors;

        public ServiceBase(IRepositoryBase<T> repository, IMapper mapper)
        {
            _repository = repository; 
            _mapper = mapper;
        }

        public async Task<T> AddAsync(TCreateDto createDto)
        {
            try
            {
                var entity = _mapper.Map<T>(createDto);
                await _repository.AddAsync(entity);
                await _repository.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _errors.Add(ex.Message);
                return null;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if(entity == null)
            {
                return false; 
            }

            return await _repository.RemoveAsync(entity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity != null ? _mapper.Map<T>(entity) : null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<T>>(entities);
        }

        public bool Validate(TCreateDto dto)
        {
            return true; 
        }

        public bool Validate(TUpdateDto dto)
        {
            return true; 
        }
    }
}

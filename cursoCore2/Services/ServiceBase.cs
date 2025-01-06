using AutoMapper;
using cursoCore2API.Repository.IRepository;

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

        public Task<T> AddAsync(TCreateDto createDto)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(int id, TUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return default; 
            }
            
            var dto = _mapper.Map<T>(entity);

            return dto;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Validate(TCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public bool Validate(TUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

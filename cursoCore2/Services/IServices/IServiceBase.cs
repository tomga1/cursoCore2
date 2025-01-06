using cursoCore2API.DTOs;

namespace cursoCore2API.Services.IServices
{
    public interface IServiceBase<T, TCreateDto, TUpdateDto>
    {
        Task<T> AddAsync(TCreateDto createDto);
        Task<T> UpdateAsync(int id, TUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();


        IReadOnlyList<string> Errors { get; }
        bool Validate(TCreateDto dto);
        bool Validate(TUpdateDto dto);


    }
}


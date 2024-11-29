using cursoCore2API.DTOs;

namespace cursoCore2API.Services
{
    public interface ICommonService<T, TI, TU>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI productoInsertDto);
        Task<T> Update(int id, TU productoUpdateDto);
        Task<T> Delete(int id);

        bool Validate(TI dto);
        bool Validate(TU dto);


    }
}


//public interface ICommonService
//{
//    Task<IEnumerable<ProductoDto>> Get();
//    Task<ProductoDto> GetById(int id);
//    Task<ProductoDto> Add(ProductoInsertDto productoInsertDto);
//    Task<ProductoDto> Update(int id, ProductoUpdateDto productoUpdateDto);
//    Task<ProductoDto> Delete(int id);


//}

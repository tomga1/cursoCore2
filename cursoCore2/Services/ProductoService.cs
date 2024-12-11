using AutoMapper;
using cursoCore2API.DTOs;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace cursoCore2API.Services
{
    public class ProductoService : ICommonService<ProductoDto, ProductoInsertDto, ProductoUpdateDto>
    {
        private IRepository<Producto> _productoRepository;
        private IMapper _mapper;
        public List<string> Errors { get; }


        public ProductoService(IRepository<Producto> productoRepository,
            IMapper mapper)
        {
            _productoRepository = productoRepository;   
            _mapper = mapper;   
        }


        public async Task<IEnumerable<ProductoDto>> Get()
        {
            var productos = await _productoRepository.Get();

            return productos.Select(p => _mapper.Map<ProductoDto>(p));
        }

        public async Task<ProductoDto> GetById(int id)
        {
            var producto = await _productoRepository.GetById(id);  

            if(producto != null)
            {
                var productoDto = _mapper.Map<ProductoDto>(producto);

                return productoDto;
            }

            return null;
        }

        public async Task<ProductoDto> Add(ProductoInsertDto productoInsertDto)
        {
            var producto = _mapper.Map<Producto>(productoInsertDto);

            await _productoRepository.Add(producto);
            await _productoRepository.Save();

            var productoDto = _mapper.Map<ProductoDto>(producto);

            return productoDto;
        }



        public async Task<ProductoDto> Update(int id, ProductoUpdateDto productoUpdateDto)
        {
            var producto = await _productoRepository.GetById(id);

            if (producto != null)
            {
                producto = _mapper.Map<ProductoUpdateDto, Producto>(productoUpdateDto,producto);    

                _productoRepository.Update(producto);
                await _productoRepository.Save();   


                var productoDto = _mapper.Map<ProductoDto>(producto);

                return productoDto;
            }

            return null;
        }





        public async Task<ProductoDto> Delete(int id)
        {
            var producto = await _productoRepository.GetById(id); 


            if (producto != null)
            {
                var productoDto = _mapper.Map<ProductoDto>(producto); 

                _productoRepository.Delete(producto);

                await _productoRepository.Save();

                

                return productoDto;
            }

            return null;

        }

        public bool Validate(ProductoInsertDto productoInsertDto)
        {
            if (_productoRepository.Search(b => b.nombre == productoInsertDto.nombre).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un nombre ya existente");
                return false; 
                
            }
            return true; 
        }

        public bool Validate(ProductoUpdateDto productoUpdateDto)
        {
            if (_productoRepository.Search(b => b.nombre == productoUpdateDto.nombre && productoUpdateDto.idProducto != b.idProducto).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un nombre ya existente");
                return false;

            }
            return true;
        }
       

        

        

        
    }
}

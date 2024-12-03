using AutoMapper;
using cursoCore2.Models;
using cursoCore2API.DTOs;
using cursoCore2API.Models;

namespace cursoCore2API.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductoInsertDto, Producto>()
                                .ForMember(dest => dest.idProducto, opt => opt.Ignore()); // Ignorar idProducto ya que es autogenerado

            CreateMap<Producto, ProductoDto>();

            CreateMap<ProductoUpdateDto, Producto>();

            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CategoriaInsertDto>().ReverseMap();    
        }
    }
}

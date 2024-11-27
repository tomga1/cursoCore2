using AutoMapper;
using cursoCore2.Models;
using cursoCore2API.DTOs;

namespace cursoCore2API.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductoInsertDto, Producto>()
                                .ForMember(dest => dest.idProducto, opt => opt.Ignore()); // Ignorar idProducto ya que es autogenerado

            CreateMap<Producto, ProductoDto>();
        }
    }
}

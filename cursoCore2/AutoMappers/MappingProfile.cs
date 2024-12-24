using AutoMapper;
using cursoCore2API.DTOs;
using cursoCore2API.Models;

namespace cursoCore2API.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<ProductoInsertDto, Producto>()
            //                    .ForMember(dest => dest.idProducto, opt => opt.Ignore()); // Ignorar idProducto ya que es autogenerado

            //CreateMap<Producto, ProductoDto>();

            //CreateMap<ProductoUpdateDto, Producto>();

            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Producto, ProductoInsertDto>().ReverseMap();
            CreateMap<Producto, ProductoUpdateDto>().ReverseMap();


            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CategoriaInsertDto>().ReverseMap();

            CreateMap<User, UsuarioDto>().ReverseMap();
            CreateMap<User, UsuarioInsertDto>().ReverseMap();

            CreateMap<AppUser, UsuarioDatosDto>().ReverseMap();
            CreateMap<AppUser, UsuarioDto>().ReverseMap();



        }
    }
}

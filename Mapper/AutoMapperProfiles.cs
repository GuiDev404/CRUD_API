using AutoMapper;
using CRUD_Basico.Dtos;
using CRUD_Basico.Models;

namespace CRUD_Basico.Mapper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //  === PRODUCTO ===
            CreateMap<ProductoCreateDTO, Producto>(); // POST O CREATE 
            // CreateMap<ProductoCreateDTO, Producto>().ReverseMap();
            CreateMap<ProductoUpdateDTO, Producto>(); // PUT O UPDATE
            CreateMap<Producto, ProductosListDTO>(); // GET O LIST
            
            //  === USUARIOS ===
            CreateMap<UsuarioRegisterDTO, Usuario>();
            CreateMap<UsuarioLoginDTO, Usuario>();
            CreateMap<Usuario, UsuariosListDTO>();

        }
    }
}
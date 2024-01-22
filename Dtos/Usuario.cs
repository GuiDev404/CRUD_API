using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Basico.Dtos
{
    public class UsuarioRegisterDTO
    {
        public UsuarioRegisterDTO()
        {
            FechaDeAlta = DateTime.Now;
            Activo = true;
        }

        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }
    }

    public class UsuarioLoginDTO
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }

    public class UsuariosListDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }
    }
}
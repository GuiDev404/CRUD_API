
namespace CRUD_Basico.Dtos
{
    public class ProductosListDTO {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }

    public class ProductoCreateDTO
    {
        public ProductoCreateDTO()
        {
            FechaDeAlta = DateTime.Now;
            Activo = true;
        }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        public DateTime FechaDeAlta { get; set; }

        public bool Activo { get; set; }
    }

    public class ProductoUpdateDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
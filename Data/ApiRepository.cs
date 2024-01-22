using CRUD_Basico.Data.Interfaces;
using CRUD_Basico.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Basico.Data
{
    public class ApiRepository : IAPIRepository
    {
        private readonly DataContext _context;

        public ApiRepository(DataContext context)
        {   
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            Producto? producto = await _context.Productos.FindAsync(id);
            return producto;
        }

        public async Task<Producto> GetProductoByNombreAsync(string nombre)
        {
            Producto? producto = await _context.Productos.FirstOrDefaultAsync(p=> p.Nombre == nombre);
            return producto;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            Usuario? usuario = await _context.Usuarios.FindAsync(id);
            return usuario;
        }

        public async Task<Usuario> GetUsuarioByNombreAsync(string nombre)
        {
            Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(p=> p.Nombre == nombre);
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            try {
                /*
                 retorna un entero que representa la cantidad de registros que han sido guardados en la base de datos. Si el valor retornado es mayor que 0, significa que al menos un cambio se guardó exitosamente; si es igual a 0, no se realizaron cambios
                */
                return await _context.SaveChangesAsync() > 0;
            } catch {
                return false;
            }
        }
    }
}
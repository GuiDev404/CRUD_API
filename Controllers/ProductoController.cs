using AutoMapper;
using CRUD_Basico.Data.Interfaces;
using CRUD_Basico.Dtos;
using CRUD_Basico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Basico.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IAPIRepository _repository;
        private readonly IMapper _mapper;

        public ProductoController(IAPIRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(){
            var productos = await _repository.GetProductosAsync();
            var productosDTOs = _mapper.Map<IEnumerable<ProductosListDTO>>(productos);
        
            return Ok(productosDTOs);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id){
            var producto = await _repository.GetProductoByIdAsync(id);
            
            if(producto == null){
                return NotFound("Producto no encontrado");
            }
 
            // var productoDTO = new ProductosListDTO(){
            //     Id = producto.Id,
            //     Descripcion = producto.Descripcion,
            //     Nombre = producto.Nombre,
            //     Precio = producto.Precio,
            // };

            var productoDTO = _mapper.Map<ProductosListDTO>(producto);
            
            return Ok(productoDTO);
        }

        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> Get(string nombre){
            var producto = await _repository.GetProductoByNombreAsync(nombre);

            if(producto == null){
                return NotFound("Producto no encontrado");
            }
            
            var productoDTO = _mapper.Map<ProductosListDTO>(producto);
            
            return Ok(productoDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductoCreateDTO productoDTO){
            // var nuevoProducto = new Producto {
            //     Nombre = productoDTO.Nombre,
            //     Descripcion = productoDTO.Descripcion,
            //     Precio = productoDTO.Precio,
            // };

            var nuevoProducto = _mapper.Map<Producto>(productoDTO);

            _repository.Add(nuevoProducto);

            bool result = await _repository.SaveAll();
            
            if(result){
                return Ok(nuevoProducto);
            }
            
            return BadRequest();
        }
  
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductoUpdateDTO productoDTO){
            if(id != productoDTO.Id){
                return BadRequest("Los IDs no coinciden");
            }

            var productToUpdate = await _repository.GetProductoByIdAsync(productoDTO.Id);
            
            if(productToUpdate == null){
                return BadRequest();
            }

            // productToUpdate.Nombre = productoDTO.Nombre;
            // productToUpdate.Descripcion = productoDTO.Descripcion;
            // productToUpdate.Precio = productoDTO.Precio;
            _mapper.Map(productoDTO, productToUpdate);

            bool result = await _repository.SaveAll();
            if(!result){
                return NoContent();
            }

            return Ok(productToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var producto = await _repository.GetProductoByIdAsync(id);

            if(producto == null){
                return NotFound("Producto no encontrado");
            }

            _repository.Delete(producto);

            bool result = await _repository.SaveAll();
            if(!result){
                return BadRequest("No se pudo eliminar el producto");
            }
            
            return Ok("Producto eliminado correctamente");
        }
    }
}
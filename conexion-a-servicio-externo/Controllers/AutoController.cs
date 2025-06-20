using BACKEND02.DTOs;
using BACKEND02.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        private StoreContext _context;
        public AutoController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet] //Buscar todos los autos
        public async Task<IEnumerable<AutoDto>> GET()
        {
            var autos = await _context.Autos
                .Select(auto => new AutoDto
                {
                    Id = auto.IdAuto,
                    NombreAuto = auto.NombreAuto,
                    Precio = auto.Precio,
                    IdMarca = auto.IdMarca
                }).ToListAsync();

            return autos;
        }


        [HttpGet("{id}")] //Buscar un auto por id
        public async Task<ActionResult<AutoDto>> GetByID (int id)
        {
            var car = await _context.Autos.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var foundCar = new AutoDto
            {
                Id = car.IdAuto,
                NombreAuto = car.NombreAuto,
                Precio = car.Precio,
                IdMarca = car.IdMarca
            };

            return Ok(foundCar);
        }

        [HttpGet("brand")] //traer todas las marcas
        public async Task<IEnumerable<MarcaDto>> GetMarcas()
        {
            var marcas = await _context.Marcas
                .Select(brand => new MarcaDto
                {
                    Id = brand.IdMarca,
                    Nombre = brand.Nombre
                }).ToListAsync();
            return marcas;
        }

        [HttpPost("brand")]
        public async Task<ActionResult<MarcaDto>> addMarca(MarcaInsertDto brand)
        {
            var marca = new Marca
            {
                Nombre = brand.Nombre
            };

            await _context.Marcas.AddAsync(marca);
            await _context.SaveChangesAsync();

            return Ok(new MarcaDto
            {
                Id = marca.IdMarca,
                Nombre = marca.Nombre
            });
        }


        [HttpPost]
        public async Task<ActionResult<AutoDto>> add(AutoInserDto auto)
        {
            var nuevoAuto = new Auto
            {
                NombreAuto = auto.NombreAuto,
                Precio = auto.Precio,
                IdMarca = auto.IdMarca
            };
            await _context.Autos.AddAsync(nuevoAuto);
            await _context.SaveChangesAsync();

            return Ok(new AutoDto
            {
                Id = nuevoAuto.IdAuto,
                NombreAuto = nuevoAuto.NombreAuto,
                Precio = nuevoAuto.Precio,
                IdMarca = nuevoAuto.IdMarca
            });
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<AutoDto>> CarUpdate(int id , AutoUpdateDto auto)
        {
            var OldCar = await _context.Autos.FindAsync(id);
            if (OldCar == null)
            {
                return NotFound();
            }

            OldCar.NombreAuto = auto.NombreAuto;
            OldCar.Precio = auto.Precio;
            OldCar.IdMarca = auto.IdMarca;
           
            await _context.SaveChangesAsync();

            return Ok(new AutoDto
            {
                Id = OldCar.IdAuto,
                NombreAuto = OldCar.NombreAuto,
                Precio = OldCar.Precio,
                IdMarca = OldCar.IdMarca
            });

        }


        [HttpDelete("{id}")] //Eliminar un auto por id
        public async Task<ActionResult> Delete(int id)
        {
            var car = await _context.Autos.FindAsync(id);
            if (car == null)
            {
                return NotFound("El auto no existe");
            }

            _context.Autos.Remove(car);
            await _context.SaveChangesAsync();

            return Ok($"Se elimno correctamente {car.NombreAuto}");

        }


    }
}

using System.ComponentModel.DataAnnotations;
using BACKEND02.DTOs;
using BACKEND02.Models;
using FluentValidation;
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
        private IValidator<AutoInserDto> _AutoInsertValidator;
        private IValidator<AutoUpdateDto> _AutoUpdateValidator;
               

        public AutoController(
            StoreContext context,
            IValidator<AutoInserDto> AutoInsertValidator,
            IValidator<AutoUpdateDto> AutoUpdateValidator)
        {
            _context = context;
            _AutoInsertValidator = AutoInsertValidator;
            _AutoUpdateValidator = AutoUpdateValidator;
           
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



        [HttpPost] //AGREGAR AUTO
        public async Task<ActionResult<AutoDto>> add(AutoInserDto auto)
        {
            //Agregamos Capa de Validaciones
            var validationResult = await _AutoInsertValidator.ValidateAsync(auto);
            if (!validationResult.IsValid)
            {
                Console.WriteLine("Error de validacion 'nombre del auto vacio' ");
                return BadRequest(validationResult.Errors);

            }


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


        [HttpPut("{id}")] //EDITAR AUTO
        public async Task<ActionResult<AutoDto>> CarUpdate( AutoUpdateDto auto, int id) //int id ,
        {
            var validation = await _AutoUpdateValidator.ValidateAsync(auto);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }


            var OldCar = await _context.Autos.FindAsync(id);
            if (OldCar == null)
            {
                Console.WriteLine("No se encontro el auto");
                return NotFound();
            }

            OldCar.NombreAuto = auto.NombreAuto;
            OldCar.Precio = auto.Precio;
            OldCar.IdMarca = auto.IdMarca;
           
            await _context.SaveChangesAsync();

            return Ok(new AutoDto
            {
                Id = id,
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

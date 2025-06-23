using BACKEND02.DTOs;
using BACKEND02.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        private IValidator<AutoInserDto> _AutoInsertValidator;
        private IValidator<AutoUpdateDto> _AutoUpdateValidator;

        //factorizacion - implementamos al Services
        private ICommonServices<AutoDto, AutoInserDto, AutoUpdateDto> _services;
               

        public AutoController(
            IValidator<AutoInserDto> AutoInsertValidator,
            IValidator<AutoUpdateDto> AutoUpdateValidator,
            [FromKeyedServices("AutoService")] ICommonServices<AutoDto, AutoInserDto, AutoUpdateDto> services)
        {
            _AutoInsertValidator = AutoInsertValidator;
            _AutoUpdateValidator = AutoUpdateValidator;
            _services = services;

        }




        [HttpGet] //Buscar todos los autos
        public async Task<IEnumerable<AutoDto>> GET() => 
            await _services.GetAll();


        [HttpGet("{id}")] //Buscar un auto por id
        public async Task<ActionResult<AutoDto>> TraerUno (int id)
        {
            var auto = await _services.GetById(id);

            return auto == null ? NotFound($"No se encontro el auto") : Ok(auto);
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

            var autoNuevo = await _services.Add(auto);

            return CreatedAtAction(nameof(TraerUno), new { id = autoNuevo.Id }, autoNuevo);

        }


        [HttpPut("{id}")] //EDITAR AUTO
        public async Task<ActionResult<AutoDto>> CarUpdate( AutoUpdateDto auto, int id) //int id ,
        {
            var validation = await _AutoUpdateValidator.ValidateAsync(auto);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var newAuto = await _services.Update(id, auto);

            return newAuto == null ? NotFound() : Ok(newAuto);

        }


        [HttpDelete("{id}")] //Eliminar un auto por id
        public async Task<ActionResult<AutoDto>> Delete(int id)
        {
            var auto = await _services.Delete(id);

            return auto == null ? NotFound() : Ok(auto);

        }


    }
}

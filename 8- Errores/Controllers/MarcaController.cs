using BACKEND02.DTOs;
using BACKEND02.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        //creamos instancia del context
       
        private ICommonServices<MarcaDto, MarcaInsertDto, MarcaUpdateDto> _marcaServices;
        //creamos instancia de las validaciones
        private IValidator<MarcaInsertDto> _marcaInsertValidator;
        private IValidator<MarcaUpdateDto> _marcaUpdateValidator;


        //contructor del controller
        public MarcaController(
            IValidator<MarcaInsertDto> marcaInsertValidator,
            IValidator<MarcaUpdateDto> marcaUpdateValidator,
            [FromKeyedServices("MarcaService")] ICommonServices<MarcaDto, MarcaInsertDto, MarcaUpdateDto> marcaServices)
        {
            _marcaServices = marcaServices;
            _marcaInsertValidator = marcaInsertValidator;
            _marcaUpdateValidator = marcaUpdateValidator;
        }

        //Crear llamados

        [HttpGet] //Buscar todas las marcas
        public async Task<IEnumerable<MarcaDto>> Get()
        {
            var marca = await _marcaServices.GetAll();
            return marca;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarcaDto>> TraerUno(int id)
        {
            var marca = await _marcaServices.GetById(id);

            return marca == null ? NotFound() : Ok(marca);
        }


        [HttpPost] //Insertar una marca
        public async Task<ActionResult<MarcaDto>> Add(MarcaInsertDto marcaI)
        {
            var validator = await _marcaInsertValidator.ValidateAsync(marcaI);
            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors);
            }

            if(!_marcaServices.Validation(marcaI))
            {
                return BadRequest(_marcaServices.Errors);
            }

            var marca = await _marcaServices.Add(marcaI);

            return CreatedAtAction(nameof(TraerUno), new { id = marca.IdMarca }, marca);

        }


        [HttpPut("{id}")] //Actualizar una marca
        public async Task<ActionResult<MarcaDto>> Update(int id, MarcaUpdateDto MarcaU)
        {
            var validator = await _marcaUpdateValidator.ValidateAsync(MarcaU);
            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors);
            }

            if ( !_marcaServices.Validation(MarcaU))
            {
                return BadRequest(_marcaServices.Errors);
            }

            var marca = await _marcaServices.Update(id, MarcaU);

            return marca == null ? NotFound($"No se encontro la marca con id {id}") : Ok(marca);
        }

        [HttpDelete("{id}")] //Eliminar una marca
        public async Task<ActionResult> Delete(int id)
        {
            var marca = await _marcaServices.Delete(id);
            return marca == null ? NotFound($"No se encontro la marca con id {id}") : Ok(marca);

        }
    }
}   

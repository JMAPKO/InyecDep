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
    public class MarcaController : ControllerBase
    {
        //creamos instancia del context
        private StoreContext _context;
        //creamos instancia de las validaciones
        private IValidator<MarcaInsertDto> _marcaInsertValidator;



        //contructor del controller
        public MarcaController(
            StoreContext context,
            IValidator<MarcaInsertDto> marcaInsertValidator)
        {
            _context = context;
            _marcaInsertValidator = marcaInsertValidator;
        }

        //Crear llamados

        [HttpGet] //Buscar todas las marcas
        public async Task<IEnumerable<MarcaDto>> Get()
        {
            
            var marcas = await _context.Marcas.Select(marcas => new MarcaDto
            {
                Id = marcas.IdMarca,
                Nombre = marcas.Nombre
            }).ToListAsync();

            return marcas;
        }

        [HttpPost] //Insertar una marca
        public async Task<ActionResult<MarcaDto>> Add(MarcaInsertDto marcaV)
        {

            var resultValidator = await _marcaInsertValidator.ValidateAsync(marcaV);
            if (!resultValidator.IsValid)
            {
                Console.WriteLine("ERROR al insertar Marca");
                return BadRequest(resultValidator.Errors);
            }

            var marca = new Marca
            {
                Nombre = marcaV.Nombre
            };

            await _context.AddAsync(marca);
            await _context.SaveChangesAsync();

            var marcaNew = new MarcaDto
            {
                Id = marca.IdMarca,
                Nombre = marca.Nombre
            };

            return Ok(new MarcaDto
            {
                Id = marcaNew.Id,
                Nombre = marcaNew.Nombre
            });

        }

    }
}   

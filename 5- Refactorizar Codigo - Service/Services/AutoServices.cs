using BACKEND02.DTOs;
using BACKEND02.Models;
using Microsoft.EntityFrameworkCore;

namespace BACKEND02.Services
{
    public class AutoServices : ICommonServices<AutoDto, AutoInserDto, AutoUpdateDto>
    {

        //creamos el contexto
        private StoreContext _context;
        //las validaciones permanecen en el controlador


        //creacion del constructor
        public AutoServices(StoreContext context)
        {
            _context = context;

        }


        public async Task<AutoDto> Add(AutoInserDto autoInsertDto)
        {

            var auto = new Auto
            {
                NombreAuto = autoInsertDto.NombreAuto,
                Precio = autoInsertDto.Precio,
                IdMarca = autoInsertDto.IdMarca
            };

            await _context.AddAsync(auto);
            await _context.SaveChangesAsync();

            var autoDto = new AutoDto
            {
                Id = auto.IdAuto,
                NombreAuto = auto.NombreAuto,
                Precio = auto.Precio,
                IdMarca = auto.IdMarca
            };

            return autoDto;


        }

        public async Task<AutoDto> Delete(int id)
        {
            var car = await _context.Autos.FindAsync(id);
            if (car != null)
            {

                var autoDto = new AutoDto
                {
                    Id = car.IdAuto,
                    NombreAuto = car.NombreAuto,
                    Precio = car.Precio,
                    IdMarca = car.IdMarca
                };

                _context.Autos.Remove(car);
                await _context.SaveChangesAsync();

                return autoDto;

            }

            return null;

        }

        public async Task<IEnumerable<AutoDto>> GetAll()
        {
            var autos = await _context.Autos.Select(a => new AutoDto
            {
                Id = a.IdAuto,
                NombreAuto = a.NombreAuto,
                Precio = a.Precio,
                IdMarca = a.IdMarca
            }).ToListAsync();

            return autos;

        }

        public async Task<AutoDto> GetById(int id)
        {
            var auto = await _context.Autos.FindAsync(id);
            if (auto != null)
            {
                var autoDTO = new AutoDto
                {
                    Id = auto.IdAuto,
                    NombreAuto = auto.NombreAuto,
                    Precio = auto.Precio,
                    IdMarca = auto.IdMarca
                };

                return autoDTO;
            }

            return null;
        }

        public async Task<AutoDto> Update(int id, AutoUpdateDto autoUpdateDto)
        {
            var auto = await _context.Autos.FindAsync(id);

            if (auto != null)
            {
                auto.NombreAuto = autoUpdateDto.NombreAuto;
                auto.Precio = autoUpdateDto.Precio;
                auto.IdMarca = autoUpdateDto.IdMarca;

                await _context.SaveChangesAsync();

                var autoDto = new AutoDto
                {
                    Id = auto.IdAuto,
                    NombreAuto = auto.NombreAuto,
                    Precio = auto.Precio,
                    IdMarca = auto.IdMarca
                };

                return autoDto;
            }

            return null;
        }
    }
}

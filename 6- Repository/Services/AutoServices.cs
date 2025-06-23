using BACKEND02.DTOs;
using BACKEND02.Models;
using BACKEND02.Repository;

namespace BACKEND02.Services
{
    public class AutoServices : ICommonServices<AutoDto, AutoInserDto, AutoUpdateDto>
    {

        //creamos el repositorio que trabaja con la BD
        private IRepository<Auto> _repository;
        //las validaciones permanecen en el controlador


        //creacion del constructor
        public AutoServices( IRepository<Auto> repository)
        {
           _repository = repository;
        }


        

        public async Task<IEnumerable<AutoDto>> GetAll()
        {
            var autos = await _repository.GetAll();

            return autos.Select(dto => new AutoDto // en el Service trabajamos con el dto, en el repository con el Modelo
            {
                Id = dto.IdAuto,
                NombreAuto = dto.NombreAuto,
                Precio = dto.Precio,
                IdMarca = dto.IdMarca
            });

        }

        public async Task<AutoDto> GetById(int id)
        {
            var auto = await _repository.GetById(id);
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

        public async Task<AutoDto> Add(AutoInserDto autoInsertDto)
        {

            var auto = new Auto
            {
                NombreAuto = autoInsertDto.NombreAuto,
                Precio = autoInsertDto.Precio,
                IdMarca = autoInsertDto.IdMarca
            };

            await _repository.Add(auto);
            await _repository.Save();

            var autoDto = new AutoDto
            {
                Id = auto.IdAuto,
                NombreAuto = auto.NombreAuto,
                Precio = auto.Precio,
                IdMarca = auto.IdMarca
            };

            return autoDto;


        }

        
        public async Task<AutoDto> Update(int id, AutoUpdateDto autoUpdateDto)
        {
            var auto = await _repository.GetById(id);

            if (auto != null)
            {
                auto.NombreAuto = autoUpdateDto.NombreAuto;
                auto.Precio = autoUpdateDto.Precio;
                auto.IdMarca = autoUpdateDto.IdMarca;

                _repository.Update(auto);
                await _repository.Save();

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

        public async Task<AutoDto> Delete(int id)
        {
            var car = await _repository.GetById(id);
            if (car != null)
            {

                var autoDto = new AutoDto
                {
                    Id = car.IdAuto,
                    NombreAuto = car.NombreAuto,
                    Precio = car.Precio,
                    IdMarca = car.IdMarca
                };

                _repository.Delete(car);
                await _repository.Save();


                return autoDto;

            }

            return null;

        }



    }
}

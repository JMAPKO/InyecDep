using AutoMapper;
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

        //AutoMaping
        private IMapper _mapper;

        //lista de errores
        public List<string> Errors { get; }


        //creacion del constructor
        public AutoServices( IRepository<Auto> repository, IMapper mapper)
        {
           _repository = repository;
            _mapper = mapper;

            //inicializamos la lista de errores
            Errors = new List<string>();
        }




        public async Task<IEnumerable<AutoDto>> GetAll()
        {
            var autos = await _repository.GetAll();

            return autos.Select(
                dto => _mapper.Map<AutoDto>(dto) // en el Service trabajamos con el dto, en el repository con el Modelo
             );

        }

        public async Task<AutoDto> GetById(int id)
        {
            var auto = await _repository.GetById(id);
            if (auto != null)
            {
                var autoDTO = _mapper.Map<AutoDto>(auto);

                return autoDTO;
            }

            return null;
        }

        public async Task<AutoDto> Add(AutoInserDto autoInsertDto)
        {

            var auto = _mapper.Map<Auto>(autoInsertDto);

          
            await _repository.Add(auto);
            await _repository.Save();

            var autoDto = _mapper.Map<AutoDto>(auto);

            return autoDto;


        }

        
        public async Task<AutoDto> Update(int id, AutoUpdateDto autoUpdateDto)
        {
            var auto = await _repository.GetById(id);

            if (auto != null)
            {
                //convertimos un DtoUpdate a un Modelo
                auto = _mapper.Map<AutoUpdateDto, Auto>(autoUpdateDto, auto);

                _repository.Update(auto);
                await _repository.Save();

                //convertimos un modelo a DTO
                var autoDto = _mapper.Map<AutoDto>(auto);

                return autoDto;
            }

            return null;
        }

        public async Task<AutoDto> Delete(int id)
        {
            var car = await _repository.GetById(id);
            if (car != null)
            {
                //usamos el mmaper para convertir un modelo a un dto
                var autoDto = _mapper.Map<AutoDto>(car);

                _repository.Delete(car);
                await _repository.Save();


                return autoDto;

            }

            return null;

        }

        public bool Validation(AutoInserDto autoInsertDto)
        {
            
            if (_repository.Search(a => a.NombreAuto == autoInsertDto.NombreAuto).Count() > 0)
            {
                Errors.Add("Ya existe un auto con ese nombre");
                return false;
            }

            return true;
        }

        public bool Validation(AutoUpdateDto autoUpdateDto)
        {
            if(_repository.Search(a => a.NombreAuto == autoUpdateDto.NombreAuto
            && a.IdMarca == autoUpdateDto.IdMarca).Count() > 0)
            {
                Errors.Add("Ya existe un auto con ese nombre y marca");
                return false;
            }

            return true;
        }
    }
}

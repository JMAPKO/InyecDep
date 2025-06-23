using AutoMapper;
using BACKEND02.DTOs;
using BACKEND02.Models;
using BACKEND02.Repository;

namespace BACKEND02.Services
{
    public class MarcaServices : ICommonServices<MarcaDto, MarcaInsertDto, MarcaUpdateDto>
    {
        private IRepository<Marca> _marcaRepository;
        private IMapper _mapper;
        

        //Lista de errores
        public List<string> Errors { get; }

        public MarcaServices(IRepository<Marca> marcaRepository, IMapper mapper)
        {
            _marcaRepository = marcaRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<MarcaDto>> GetAll()
        {
            var marcas = await _marcaRepository.GetAll();

            return marcas.Select(m => _mapper.Map<MarcaDto>(m)
            );
        }
       

        public async Task<MarcaDto> GetById(int id)
        {
            var marca = await _marcaRepository.GetById(id);
            if (marca != null)
            {
                var marcaDto = _mapper.Map<MarcaDto>(marca);
                return marcaDto;
                
            }

            return null;
        }

        public async Task<MarcaDto> Add(MarcaInsertDto MarcaDtoInsert)
        {
            var marca = _mapper.Map<Marca>(MarcaDtoInsert);

            await _marcaRepository.Add(marca);
            await _marcaRepository.Save();

            var marcaDto = _mapper.Map<MarcaDto>(marca);
            return marcaDto;

        }

        public async Task<MarcaDto> Update(int id, MarcaUpdateDto marcaDtoUpdate)
        {
            var marca = await _marcaRepository.GetById(id);
            if (marca != null)
            {
                marca.NombreMarca = marcaDtoUpdate.NombreMarca;
                _marcaRepository.Update(marca);
                await _marcaRepository.Save();

                var MarcaDto = _mapper.Map<MarcaDto>(marca);
                return MarcaDto;
            }

            return null;
        }




        public async Task<MarcaDto> Delete(int id)
        {
            var marca = await _marcaRepository.GetById(id);

            if (marca != null)
            {
                var marcaBorrada = _mapper.Map<MarcaDto>(marca);

                _marcaRepository.Delete(marca);
                await _marcaRepository.Save();
                return marcaBorrada;
            }

            return null;
        }

        public bool Validation(MarcaInsertDto marcaDtoInsert)
        {
            if (_marcaRepository.Search(m => m.NombreMarca == marcaDtoInsert.NombreMarca).Count() > 0)
            {
                Errors.Add("Ya existe una marca con ese MISMO nombre.");
                return false;
            }

            return true;
        }

        public bool Validation(MarcaUpdateDto marcaDtoUpdate)
        {
            if (_marcaRepository.Search(m => m.NombreMarca == marcaDtoUpdate.NombreMarca
            ).Count() > 0)
            {
                Errors.Add("Ya existe una marca con ese MISMO nombre.");
                return false;
            }

            return true;
        }

        
    }
}

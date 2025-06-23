using AutoMapper;
using BACKEND02.DTOs;
using BACKEND02.Models;
using BACKEND02.Repository;

namespace BACKEND02.Services
{
    public class MarcaServices : ICommonServices<MarcaDto, MarcaDto, MarcaDto>
    {
        private IRepository<Marca> _marcaRepository;
        private IMapper _mapper;

        public MarcaServices(IRepository<Marca> marcaRepository, IMapper mapper)
        {
            _marcaRepository = marcaRepository;
            _mapper = mapper;
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

        public async Task<MarcaDto> Add(MarcaDto MarcaDtoInsert)
        {
            var marca = _mapper.Map<Marca>(MarcaDtoInsert);

            await _marcaRepository.Add(marca);
            await _marcaRepository.Save();

            var marcaDto = _mapper.Map<MarcaDto>(marca);
            return marcaDto;

        }

        public async Task<MarcaDto> Update(int id, MarcaDto MarcaDtoUpdate)
        {
            var marca = await _marcaRepository.GetById(id);
            if (marca != null)
            {
                marca.NombreMarca = MarcaDtoUpdate.NombreMarca;
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

        
    }
}

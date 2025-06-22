using BACKEND02.DTOs;
using BACKEND02.Models;
using BACKEND02.Repository;

namespace BACKEND02.Services
{
    public class MarcaServices : ICommonServices<MarcaDto, MarcaDto, MarcaDto>
    {
        private IRepository<Marca> _marcaRepository;

        public MarcaServices(IRepository<Marca> marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<IEnumerable<MarcaDto>> GetAll()
        {
            var marcas = await _marcaRepository.GetAll();

            return marcas.Select(m => new MarcaDto
            {
                IdMarca = m.IdMarca,
                NombreMarca = m.NombreMarca
            });
        }
       

        public async Task<MarcaDto> GetById(int id)
        {
            var marca = await _marcaRepository.GetById(id);
            if (marca != null)
            {
                return new MarcaDto
                {
                    IdMarca = marca.IdMarca,
                    NombreMarca = marca.NombreMarca
                };
            }

            return null;
        }

        public async Task<MarcaDto> Add(MarcaDto MarcaDtoInsert)
        {
            var marca = new Marca
            {
                NombreMarca = MarcaDtoInsert.NombreMarca
            };
            
            await _marcaRepository.Add(marca);
            await _marcaRepository.Save();

            return new MarcaDto
            {
                IdMarca = marca.IdMarca,
                NombreMarca = marca.NombreMarca
            };

        }

        public async Task<MarcaDto> Update(int id, MarcaDto MarcaDtoUpdate)
        {
            var marca = await _marcaRepository.GetById(id);
            if (marca != null)
            {
                marca.NombreMarca = MarcaDtoUpdate.NombreMarca;
                _marcaRepository.Update(marca);
                await _marcaRepository.Save();

                return new MarcaDto
                {
                    IdMarca = marca.IdMarca,
                    NombreMarca = marca.NombreMarca
                };
            }

            return null;
        }




        public async Task<MarcaDto> Delete(int id)
        {
            var marca = await _marcaRepository.GetById(id);

            if (marca != null)
            {
                var marcaBorrada = new MarcaDto
                {
                    IdMarca = marca.IdMarca,
                    NombreMarca = marca.NombreMarca
                };
                 _marcaRepository.Delete(marca);
                await _marcaRepository.Save();
                return marcaBorrada;
            }

            return null;
        }

        
    }
}

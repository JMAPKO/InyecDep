using AutoMapper;
using BACKEND02.DTOs;
using BACKEND02.Models;

namespace BACKEND02.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<AutoInserDto, Auto>();

            CreateMap<Auto, AutoDto>().ForMember(
                m => m.Id, //Valor que quiero asignar
                autoDto =>  autoDto.MapFrom(a => a.IdAuto)); //Destino del mapeo

            CreateMap<AutoDto, Auto>().ForMember(
                destino => destino.IdAuto,
                auto => auto.MapFrom(a => a.Id));

            //Update auto - ingnorar el id porque no queremos que se modifique
            CreateMap<AutoUpdateDto, Auto>()
                .ForMember(dest => dest.IdAuto, opt => opt.Ignore())
                .ForMember(dest => dest.Marca, opt => opt.Ignore());


            //Mapeo para Marca
            CreateMap<Marca, MarcaDto>()
                .ReverseMap();
        }

    }
}

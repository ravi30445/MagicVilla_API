
using AutoMapper;
using MagicVilla_webapi.Models.Dto;
using MagicVilla_webapi.Models;


namespace MagicVilla_webapi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO,Villa>();

            CreateMap<Villa, MagicVilla_VillaAPI.Models.Dto.VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, MagicVilla_VillaAPI.Models.Dto.VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDTO>();
            CreateMap<VillaNumberDTO,VillaNumber>();

            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();

          
        }
    }
}
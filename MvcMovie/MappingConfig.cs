
using AutoMapper;
using MvcMovie.Models.Dto;
using MvcMovie.Models;


namespace MvcMovie
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO, VillaCreateDTO>();
            CreateMap<VillaCreateDTO,VillaDTO>();
            CreateMap<VillaDTO,VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();

          
        }
    }
}
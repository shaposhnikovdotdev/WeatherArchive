using Application.Common.DTOs;
using AutoMapper;
using Context.Entities;

namespace Application.Common.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<WeatherDto, Weather>().ReverseMap();
            CreateMap<PaginationResult<WeatherDto>, PaginationResult<Weather>>().ReverseMap();
        }
    }
}

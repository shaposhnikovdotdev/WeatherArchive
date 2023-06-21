using Application.Common;
using Application.Common.DTOs;
using AutoMapper;
using Context.Entities;
using Context.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetWeatherDto : IRequest<PaginationResult<WeatherDto>>
    {
        // default page size
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public DateTime Date { get; set; }
    }
    public class GetWeatherDtoHandler : IRequestHandler<GetWeatherDto, PaginationResult<WeatherDto>>
    {
        IMapper _mapper;
        IWeatherRepository _weatherRepository;
        List<Weather> _weatherList;
        GetWeatherDto _requst;
        public GetWeatherDtoHandler(IMapper mapper, IWeatherRepository weatherRepository)
        {
            _mapper = mapper;
            _weatherRepository = weatherRepository;
        }
        public async Task<PaginationResult<WeatherDto>> Handle(GetWeatherDto request, CancellationToken cancellationToken)
        {
            _requst = request;

            _weatherList = await _weatherRepository.GetAll();
            FilteringByDate();

            var _mappedList = _mapper.Map<PaginationResult<WeatherDto>>(MakePagination());

            return _mappedList;
        }
        PaginationResult<Weather> MakePagination()
        {
            var count = _weatherList.Count;
            var items = _weatherList.Skip((_requst.PageNumber - 1) * _requst.PageSize).Take(_requst.PageSize).ToList();

            return new PaginationResult<Weather>() { Count = count, Items = items, PageSize = _requst.PageSize, PageNumber = _requst.PageNumber };
        }
        void FilteringByDate()
        {
            if (_requst.Date != null && _requst.Date != default(DateTime))
            {
                _weatherList = _weatherList.Where(w => w.DateTime.Year == _requst.Date.Year
                && w.DateTime.Month == _requst.Date.Month
                && _requst.Date.Day == w.DateTime.Day)
                    .ToList();
            }
        }
    }
}

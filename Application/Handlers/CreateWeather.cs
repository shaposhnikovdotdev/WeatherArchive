using AutoMapper;
using Context.Entities;
using Context.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateWeather : IRequest<int>
    {
        public IFormFileCollection Files { get; set; }
    }
    public class CreateWeatherHandler : IRequestHandler<CreateWeather, int>
    {
        IMediator _mediator;
        IMapper _mapper;
        IWeatherRepository _weatherRepository;
        public CreateWeatherHandler(IMediator mediator, IMapper mapper, IWeatherRepository weatherRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _weatherRepository = weatherRepository;
        }

        public async Task<int> Handle(CreateWeather request, CancellationToken cancellationToken)
        {
            var dtoList = await _mediator.Send(new ReadWeatherXls() { Files = request.Files });
            var models = _mapper.Map<List<Weather>>(dtoList);
            var result = await _weatherRepository.Create(models);
            return result;
        }
    }
}

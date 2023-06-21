using Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Interfaces
{
    public interface IWeatherRepository
    {
        Task<int> Create(List<Weather> models);
        Task<List<Weather>> GetAll();
    }
}

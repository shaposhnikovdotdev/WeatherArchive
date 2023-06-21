using Context.Entities;
using Context.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Context.Repository
{
    public class WeatherRepository : IWeatherRepository
    {
        ApplicationContext _dbContext;
        public WeatherRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(List<Weather> models)
        {
            await _dbContext.AddRangeAsync(models);
            
            var result = await _dbContext.SaveChangesAsync();

            return result;
        }
        public async Task<List<Weather>> GetAll()
        {
            return await _dbContext.Weather.ToListAsync();
        }
    }
}

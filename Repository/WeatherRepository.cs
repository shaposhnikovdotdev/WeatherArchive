using Context;
using Context.Entities;

namespace Repository
{
    public class WeatherRepository
    {
        ApplicationContext _dbContext;
        public WeatherRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(List<Weather> models)
        {
            foreach(var model in models)
            {
                await _dbContext.AddRangeAsync(models);
            }
            var result = await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
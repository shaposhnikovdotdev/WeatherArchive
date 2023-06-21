using Application.Common.DTOs;

namespace WeatherArchiveMvc.Models
{
    public class IndexViewModel
    {
        public IEnumerable<WeatherDto> Weathers { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public DateTime? Date { get; set; }
    }
}

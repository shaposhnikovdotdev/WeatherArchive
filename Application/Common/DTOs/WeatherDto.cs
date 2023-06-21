using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTOs
{
    public class WeatherDto
    {
        public DateTime DateTime { get; set; }
        public double Temperature { get; set; }
        public double AirHumidity { get; set; }
        public double DewPoint { get; set; }
        public int AirPressure { get; set; }
        public string? WindDirection { get; set; }
        public int? WindSpeed { get; set; }
        public int Cloudiness { get; set; }
        public int minCloudiness { get; set; }
        public int HorizontalVisibility { get; set; }
        public string? Event { get; set; }
    }
}

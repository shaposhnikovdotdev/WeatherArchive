using Application.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class ReadWeatherXls : IRequest<List<WeatherDto>>
    {
        public IFormFileCollection Files { get; set; }
    }
    public class ReadWeatherXlsHandler : IRequestHandler<ReadWeatherXls, List<WeatherDto>>
    {
        public async Task<List<WeatherDto>> Handle(ReadWeatherXls request, CancellationToken cancellationToken)
        {
            IWorkbook workbook = null;
            List<WeatherDto> result = new List<WeatherDto>();
            foreach(var file in request.Files)
            {
                if (file.FileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(file.OpenReadStream());
                }
                else if (file.FileName.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(file.OpenReadStream());
                }

                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    if (sheet != null)
                    {
                        int rowCount = sheet.LastRowNum;
                        for(int j = 4; j <= rowCount; j++)
                        {
                            int toParse;

                            IRow currentRow = sheet.GetRow(j);

                            var weather = new WeatherDto();

                            var dateString = currentRow.GetCell(0).StringCellValue.Trim();
                            var timeString = currentRow.GetCell(1).StringCellValue.Trim();
                            weather.DateTime = DateTime.ParseExact(dateString + " " + timeString, "dd.MM.yyyy HH:mm",
                                       System.Globalization.CultureInfo.InvariantCulture);

                            weather.Temperature = currentRow.GetCell(2).NumericCellValue;
                            weather.AirHumidity = currentRow.GetCell(3).NumericCellValue;
                            weather.DewPoint = currentRow.GetCell(4).NumericCellValue;
                            weather.WindDirection = currentRow.GetCell(6).StringCellValue.Trim();

                            int.TryParse(currentRow.GetCell(5).ToString(), out toParse);
                            weather.AirPressure = toParse;

                            int.TryParse(currentRow.GetCell(7).ToString(), out toParse);
                            weather.WindSpeed = toParse;

                            int.TryParse(currentRow.GetCell(8).ToString(), out toParse);
                            weather.Cloudiness = toParse;

                            int.TryParse(currentRow.GetCell(9).ToString(), out toParse);
                            weather.minCloudiness = toParse;

                            int.TryParse(currentRow.GetCell(10).ToString(), out toParse);
                            weather.HorizontalVisibility = toParse;

                            weather.Event = currentRow.GetCell(11) != null ? currentRow.GetCell(11).ToString() : null;

                            result.Add(weather);
                        }
                    }
                }
            }
            return result;
            
        }
    }
}

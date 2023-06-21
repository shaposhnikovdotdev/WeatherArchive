using Application.Common;
using Application.Common.DTOs;
using Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO;
using WeatherArchiveMvc.Models;

namespace WeatherArchiveMvc.Controllers
{
    public class HomeController : Controller
    {
        IMediator _mediator;
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int page = 1, DateTime date = default(DateTime))
        {
            var paginationData = await GetData(page, date);

            PageViewModel pageViewModel = new PageViewModel(paginationData.Count, paginationData.PageNumber, paginationData.PageSize, date);

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Weathers = paginationData.Items
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Upload()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task<PaginationResult<WeatherDto>> GetData(int page, DateTime date)
        {
            return await _mediator.Send(new GetWeatherDto() { PageNumber = page, Date = date });
        }
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(CreateWeather query)
        {
            await _mediator.Send(query);
            return Redirect("Home");
        }
    }
}
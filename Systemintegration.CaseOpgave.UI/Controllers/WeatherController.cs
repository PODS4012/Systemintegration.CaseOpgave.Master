using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Systemintegration.CaseOpgave.Service.Contracts;
using Systemintegration.CaseOpgave.Shared;
using Systemintegration.CaseOpgave.UI.Models;
using VisualCrossingsService;

namespace Systemintegration.CaseOpgave.UI.Controllers
{
    public class WeatherController : Controller
    {
       
        private readonly IWeatherService service;

        public WeatherController(IWeatherService service)
        {
            this.service = service;
        }
        public IActionResult Index() => View(this.service.GetWeatherParams());

    }
}

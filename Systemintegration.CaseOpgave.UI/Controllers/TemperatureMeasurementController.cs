using Microsoft.AspNetCore.Mvc;
using Systemintegration.CaseOpgave.Service.Contracts;

namespace Systemintegration.CaseOpgave.UI.Controllers
{
    public class TemperatureMeasurementController : Controller
    {
        private readonly ITemperatureMeasurementService service;

        public TemperatureMeasurementController(ITemperatureMeasurementService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index() => View(await service.ListAllTodayAsync());
    }
}

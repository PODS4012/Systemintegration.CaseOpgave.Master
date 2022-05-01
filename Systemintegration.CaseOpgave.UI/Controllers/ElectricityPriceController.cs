using Microsoft.AspNetCore.Mvc;
using Systemintegration.CaseOpgave.Service.Contracts;

namespace Systemintegration.CaseOpgave.UI.Controllers
{
    public class ElectricityPriceController : Controller
    {
        private readonly IElectricityPriceService service;

        public ElectricityPriceController(IElectricityPriceService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
             double price = this.service.GetElectricityPrice().Result;
            return View((object?)price);
        }
    }
}

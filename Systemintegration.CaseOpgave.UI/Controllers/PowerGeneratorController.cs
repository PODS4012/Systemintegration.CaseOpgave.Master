using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Systemintegration.CaseOpgave.Service.Contracts;

namespace Systemintegration.CaseOpgave.UI.Controllers
{
    public class PowerGeneratorController : Controller
    {
        private readonly IPowerGeneratorService service;

        public PowerGeneratorController(IPowerGeneratorService service)
        {
            this.service = service;
        }

        public IActionResult Index() => View(this.service.GetPowerGeneratorParams());
    }
}

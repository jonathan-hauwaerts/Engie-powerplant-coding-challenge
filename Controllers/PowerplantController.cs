using Engie_powerplant_coding_challenge.Models;
using Engie_powerplant_coding_challenge.Services;
using Engie_powerplant_coding_challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Engie_powerplant_coding_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerplantController : Controller
    {
        private readonly IPowerplantCalculator _powerplantCalculator;
        public PowerplantController(IPowerplantCalculator powerplantCalculator)
        {
            _powerplantCalculator = powerplantCalculator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost(Name = "productionplan")]
        public IEnumerable<PowerplantProductionPlan> productionplan(Payload payload)
        {
            return _powerplantCalculator.GetProductionPlan(payload);
        }
    }
}

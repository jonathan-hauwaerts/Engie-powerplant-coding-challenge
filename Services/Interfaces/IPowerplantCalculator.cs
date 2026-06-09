using Engie_powerplant_coding_challenge.Models;

namespace Engie_powerplant_coding_challenge.Services.Interfaces
{
    public interface IPowerplantCalculator
    {
        public List<PowerplantProductionPlan> GetProductionPlan(Payload payload);
    }
}

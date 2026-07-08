using Engie_powerplant_coding_challenge.Models;
using Engie_powerplant_coding_challenge.Models.Enums;
using Engie_powerplant_coding_challenge.Services.Interfaces;
using System.Numerics;

namespace Engie_powerplant_coding_challenge.Services
{
    public class PowerplantCalculator : IPowerplantCalculator
    {
        public PowerplantCalculator()
        {

        }

        public List<PowerplantProductionPlan> GetProductionPlan(Payload payload)
        {
            //
            List<Powerplant> powerplants = CalculateProductionCost(payload.Powerplants, payload.Fuels).OrderBy(x => x.ProductionCost).ToList();

            powerplants.Where(x => x.Type == PowerplantType.windturbine).ToList().ForEach(x => x.Pmax *= payload.Fuels.Wind / 100);

            return CreateProductionplan(powerplants, payload.Load);
        }

        private List<Powerplant> CalculateProductionCost(List<Powerplant> powerplants, Fuel fuels)
        {

            foreach (Powerplant powerplant in powerplants.Where(x => x.Type != PowerplantType.windturbine))
            {
                switch (powerplant.Type)
                {
                    case PowerplantType.gasfired:
                        powerplant.ProductionCost = fuels.Gas / powerplant.Efficiency + (0.3 * fuels.Co2) / powerplant.Efficiency;
                        break;
                    case PowerplantType.turbojet:
                        powerplant.ProductionCost = fuels.Kerosine / powerplant.Efficiency;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return powerplants;
        }

        private List<PowerplantProductionPlan> CreateProductionplan(List<Powerplant> powerplants, double load)
        {

            double remainingLoad = load;

            List<PowerplantProductionPlan> powerplantResults = new List<PowerplantProductionPlan>();

            for (int i = 0; i < powerplants.Count; i++)
            {
                Powerplant powerplant = powerplants[i];

                PowerplantProductionPlan powerplantProductionPlan = 
                    new PowerplantProductionPlan() { 
                    Name = powerplant.Name, 
                    Production = 0};

                if (remainingLoad > 0 && load > powerplant.Pmin)
                {
                    //check to see what the min production is of the next powerplant
                    double pminNext = powerplant.Pmin;
                    if (i + 1 < powerplants.Count)
                        pminNext = powerplants[i + 1].Pmin;

                    //Check to see if the production we'll use in this powerplant leaves enough open for the next powerplant
                    if (remainingLoad - powerplant.Pmax < pminNext && remainingLoad - powerplant.Pmax > 0 && remainingLoad - pminNext >= 0)
                    {
                        powerplantProductionPlan.Production = remainingLoad - pminNext;
                    }
                    else if (remainingLoad > powerplant.Pmax)
                    {
                        powerplantProductionPlan.Production = powerplant.Pmax;
                    }
                    else if (powerplant.Type != PowerplantType.windturbine && remainingLoad > powerplant.Pmin)
                    {
                        powerplantProductionPlan.Production = remainingLoad;
                    }

                    //subtract the production from the load
                    remainingLoad -= powerplantProductionPlan.Production;
                }

                powerplantResults.Add(powerplantProductionPlan);
            }

            //check if there is still something left
            if (remainingLoad > 0)
            {
                //check if there is a powerplant that can solve the load if the production was reassigned
                Powerplant? powerplant = powerplants.Where(x => x.Pmin < load && x.Pmax > load && x.Type != PowerplantType.windturbine).FirstOrDefault();
                if (powerplant == null)
                    throw new InvalidOperationException("No productionplan possible");
                else
                {
                    powerplantResults.ForEach(x => x.Production = 0);
                    powerplantResults.Where(x => x.Name == powerplant.Name).First().Production = load;
                }
            }
            return powerplantResults;
        }
    }
}

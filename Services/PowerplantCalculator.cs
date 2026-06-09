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
            double load = payload.Load;

            try
            {
                CalculateProductionCost(ref payload);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            List<Powerplant> powerplants = payload.Powerplants.OrderBy(x => x.ProductionCost).ToList();

            List<PowerplantProductionPlan> powerplantResults = new List<PowerplantProductionPlan>();

            for (int i = 0; i < powerplants.Count; i++)
            {
                
                Powerplant powerplant = powerplants[i];

                
                PowerplantProductionPlan powerplantProductionPlan = new PowerplantProductionPlan();
                powerplantProductionPlan.Name = powerplant.Name;
                powerplantProductionPlan.p = 0;


                if (load > 0 && load > powerplant.Pmin)
                {
                    //check to see what the min production is of the next powerplant
                    double pminNext = powerplant.Pmin;
                    if (i + 1 < powerplants.Count)
                        pminNext = powerplants[i + 1].Pmin;

                    //Check to see if the production we'll use in this powerplant leaves enough open for the next powerplant
                    if (load - powerplant.Pmax < pminNext && load - powerplant.Pmax > 0)
                    {
                        powerplantProductionPlan.p = load - pminNext;
                    }
                    else if (load > powerplant.Pmax)
                    {
                        powerplantProductionPlan.p = powerplant.Pmax;
                    }
                    else if (powerplant.Type != PowerplantType.windturbine )
                    {
                        powerplantProductionPlan.p = load;
                    }

                    //calculations for the production of windturbines
                    if (powerplant.Type == PowerplantType.windturbine)
                    {

                        powerplantProductionPlan.p *= (payload.Fuels.wind / 100);
                        if (powerplantProductionPlan.p > load)
                        {
                            powerplantProductionPlan.p = 0;
                            continue;
                        }
                            
                    }

                    load -= powerplantProductionPlan.p;
                }


                powerplantResults.Add(powerplantProductionPlan);
            }

            if (load > 0)
                throw new InvalidOperationException("No productionplan possible");

            return powerplantResults;
        }

        private void CalculateProductionCost (ref Payload payload)
        {
            foreach (Powerplant powerplant in payload.Powerplants.Where(x => x.Type != PowerplantType.windturbine))
            {
                switch (powerplant.Type)
                {
                    case PowerplantType.gasfired:
                        powerplant.ProductionCost = payload.Fuels.gas / powerplant.Efficiency + (0.3 * payload.Fuels.co2) / powerplant.Efficiency;
                        break;
                    case PowerplantType.turbojet:
                        powerplant.ProductionCost = payload.Fuels.kerosine / powerplant.Efficiency;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }
    }
}

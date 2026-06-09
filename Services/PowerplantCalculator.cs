using Engie_powerplant_coding_challenge.Models;
using Engie_powerplant_coding_challenge.Models.Enums;
using Engie_powerplant_coding_challenge.Services.Interfaces;

namespace Engie_powerplant_coding_challenge.Services
{
    public class PowerplantCalculator : IPowerplantCalculator
    {
        public PowerplantCalculator()
        {

        }

        public List<PowerplantProductionPlan> GetProductionPlan(Payload payload)
        {
            Single load = payload.Load;

            List<Powerplant> powerplants = payload.Powerplants.OrderByDescending(x => x.Efficiency).ToList();

            List<PowerplantProductionPlan> powerplantResults = new List<PowerplantProductionPlan>();

            for (int i = 0; i < powerplants.Count; i++)
            {

                Powerplant powerplant = powerplants[i];

                PowerplantProductionPlan powerplantProductionPlan = new PowerplantProductionPlan();
                powerplantProductionPlan.Name = powerplant.Name;
                powerplantProductionPlan.p = 0;


                if (load > 0)
                {
                    //check to see what the min production is of the next powerplant
                    Single pminNext = powerplant.Pmin;
                    if (i + 1 < powerplants.Count)
                        pminNext = powerplants[i + 1].Pmin;

                    //Check to see if the production we'll use in this powerplant leaves enough open for the next powerplant
                    if (load - powerplant.Pmax < pminNext && load - powerplant.Pmax > 0)
                    {
                        powerplantProductionPlan.p = load - 120;
                    }
                    else if (load > powerplant.Pmax)
                    {
                        powerplantProductionPlan.p = powerplant.Pmax;
                    }
                    else
                    {
                        powerplantProductionPlan.p = load;
                    }

                    //calculations for the production of windturbines
                    switch (powerplant.Type)
                    {
                        case PowerplantType.windturbine:
                            load -= powerplantProductionPlan.p * (payload.Fuels.wind / 100);
                            break;
                        default:
                            load -= powerplantProductionPlan.p;
                            break;
                    }
                }


                powerplantResults.Add(powerplantProductionPlan);
            }

            return powerplantResults;
        }
    }
}

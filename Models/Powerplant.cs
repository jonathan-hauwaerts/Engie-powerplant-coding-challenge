using Engie_powerplant_coding_challenge.Helpers;
using Engie_powerplant_coding_challenge.Models.Enums;
using Microsoft.AspNetCore.CookiePolicy;
using System.Text.Json.Serialization;

namespace Engie_powerplant_coding_challenge.Models
{
    public class Powerplant
    {
        public string Name { get; set; }
        [JsonConverter(typeof(PowerplantTypeConverter))]
        public PowerplantType Type { get; set; }
        public double Efficiency { get; set; }
        public int Pmin { get; set; }
        public int Pmax { get; set; }
        public double ProductionCost { get; set; }
    }
}

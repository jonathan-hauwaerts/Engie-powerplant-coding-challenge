using Engie_powerplant_coding_challenge.Helpers;
using Engie_powerplant_coding_challenge.Models.Enums;
using Microsoft.AspNetCore.CookiePolicy;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Engie_powerplant_coding_challenge.Models
{
    public class Powerplant
    {
        public required string Name { get; set; }
        [JsonConverter(typeof(PowerplantTypeConverter))]
        public required PowerplantType Type { get; set; }
        public required double Efficiency { get; set; }
        public required double Pmin { get; set; }
        public required double Pmax { get; set; }
        public double ProductionCost { get; set; }
    }
}

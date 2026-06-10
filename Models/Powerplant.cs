using Engie_powerplant_coding_challenge.Helpers;
using Engie_powerplant_coding_challenge.Models.Enums;
using Microsoft.AspNetCore.CookiePolicy;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Engie_powerplant_coding_challenge.Models
{
    public class Powerplant
    {
        [Required]
        public required string Name { get; set; }
        [JsonConverter(typeof(PowerplantTypeConverter))]
        [Required]
        public PowerplantType Type { get; set; }
        [Required]
        public double Efficiency { get; set; }
        [Required]
        public double Pmin { get; set; }
        [Required]
        public double Pmax { get; set; }
        public double ProductionCost { get; set; }
    }
}

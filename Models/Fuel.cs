using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Engie_powerplant_coding_challenge.Models
{
    public class Fuel
    {
        [Required]
        [JsonPropertyName("gas(euro/MWh)")]
        public double Gas { get; set; }
        [Required]
        [JsonPropertyName("kerosine(euro/MWh)")]
        public double Kerosine { get; set; }
        [Required]
        [JsonPropertyName("co2(euro/ton)")]
        public double Co2 { get; set; }
        [Required]
        [Range(0, 100)]
        [JsonPropertyName("wind(%)")]
        public double Wind { get; set; }
    }
}

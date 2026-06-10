using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Engie_powerplant_coding_challenge.Models
{
    public class Fuel
    {
        
        [JsonPropertyName("gas(euro/MWh)")]
        public required double Gas { get; set; }
        [JsonPropertyName("kerosine(euro/MWh)")]
        public required double Kerosine { get; set; }
        [JsonPropertyName("co2(euro/ton)")]
        public required double Co2 { get; set; }
        [Range(0, 100)]
        [JsonPropertyName("wind(%)")]
        public required double Wind { get; set; }
    }
}

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Engie_powerplant_coding_challenge.Models
{
    public class Fuel
    {
        [JsonPropertyName("gas(euro/MWh)")]
        public Single gas { get; set; }
        [JsonPropertyName("kerosine(euro/MWh)")]
        public Single kerosine { get; set; }
        [JsonPropertyName("co2(euro/ton)")]
        public Single co2 { get; set; }
        [JsonPropertyName("wind(%)")]
        public Single wind { get; set; }
    }
}

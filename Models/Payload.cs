using System.ComponentModel.DataAnnotations;

namespace Engie_powerplant_coding_challenge.Models
{
    public class Payload
    {
        public required int Load { get; set; }
        public required Fuel Fuels { get; set; }
        public required List<Powerplant> Powerplants { get; set; }
    }
}

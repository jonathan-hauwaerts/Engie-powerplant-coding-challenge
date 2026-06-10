using System.ComponentModel.DataAnnotations;

namespace Engie_powerplant_coding_challenge.Models
{
    public class Payload
    {
        [Required]
        public int Load { get; set; }
        public required Fuel Fuels { get; set; }
        public required List<Powerplant> Powerplants { get; set; }
    }
}

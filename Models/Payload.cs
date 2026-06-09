namespace Engie_powerplant_coding_challenge.Models
{
    public class Payload
    {
        public int Load { get; set; }
        public Fuel Fuels { get; set; }
        public List<Powerplant> Powerplants { get; set; }
    }
}

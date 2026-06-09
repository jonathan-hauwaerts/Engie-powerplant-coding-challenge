using System.Runtime.Serialization;

namespace Engie_powerplant_coding_challenge.Models.Enums
{
    public enum PowerplantType
    {
        [EnumMember(Value = "gasfired")]
        gasfired,
        [EnumMember(Value = "turbojet")]
        turbojet,
        [EnumMember(Value = "windturbine")]
        windturbine
    }
}

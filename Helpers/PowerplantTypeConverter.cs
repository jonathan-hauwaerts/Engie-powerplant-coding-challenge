using Engie_powerplant_coding_challenge.Models.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Engie_powerplant_coding_challenge.Helpers
{
    public class PowerplantTypeConverter : JsonConverter<PowerplantType>
    {
        public override PowerplantType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (Enum.TryParse<PowerplantType>(value, true, out var result))
                return result;

           
            throw new JsonException(
                $"Invalid plant type '{value}'. Allowed values: {string.Join(", ", Enum.GetNames<PowerplantType>())}");
        }

        public override void Write(Utf8JsonWriter writer, PowerplantType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}

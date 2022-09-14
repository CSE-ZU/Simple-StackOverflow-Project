using System.Text.Json.Serialization;

namespace OnBoarding.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Count
{
    Down = -1,
    Up = 1
}
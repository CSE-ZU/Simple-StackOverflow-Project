using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OnBoarding.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CountType 
{
    [EnumMember(Value = "Down")]
    Down, 
    [EnumMember(Value = "Up")]
    Up
}
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BL;

public class CountryDTO
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("dial_code")]
    public string? Dial_code { get; set; }

    [JsonProperty("code")]
    public string? Code { get; set; }
}
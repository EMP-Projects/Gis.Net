using System.Text.Json.Serialization;

namespace Gis.Net.Osm.Overpass.Dto;

public class OverPassResponseDto
{
        [JsonPropertyName("version")]
        public double? Version;
        
        [JsonPropertyName("generator")]
        public string? Generator;

        [JsonPropertyName("osm3s")]
        public Osm3S? Osm3S;

        [JsonPropertyName("elements")]
        public List<Element?>? Elements;
}
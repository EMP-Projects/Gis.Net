namespace Gis.Net.OpenMeteo.Weather;

public interface IGeocodingResponse
{
    List<double?> Elevation { get; set; }
}
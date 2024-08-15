namespace Gis.Net.OpenMeteo.Weather;

public class CoordinatesOptions : ICoordinatesOptions
{
    /// <inheritdoc />
    public double? Lat { get; set; }

    /// <inheritdoc />
    public double? Lng { get; set; }
}
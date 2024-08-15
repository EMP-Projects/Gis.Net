namespace Gis.Net.OpenMeteo.Weather;

/// <summary>
/// 
/// </summary>
public static class WeatherCodes
{
    /// <summary>
    /// WMO Weather interpretation codes (WW)
    /// </summary>
    private static Dictionary<List<int>, string>? WmoCodes { get; set; } = new () {
        { new List<int> { 0 }, "Clear sky" },
        { new List<int> { 1,2,3 }, "Mainly clear, partly cloudy, and overcast" },
        { new List<int> { 45, 58 }, "Fog and depositing rime fog" },
        { new List<int> { 51, 53, 55 }, "Drizzle: Light, moderate, and dense intensity" },
        { new List<int> { 56, 57 }, "Freezing Drizzle: Light and dense intensity" },
        { new List<int> { 61, 63, 65 }, "Rain: Slight, moderate and heavy intensity" },
        { new List<int> { 66, 67 }, "Freezing Rain: Light and heavy intensity" },
        { new List<int> { 71, 73, 75 }, "Snow fall: Slight, moderate, and heavy intensity" },
        { new List<int> { 77 }, "Snow grains" },
        { new List<int> { 80, 81, 82 }, "Rain showers: Slight, moderate, and violent" },
        { new List<int> { 85, 86 }, "Snow showers slight and heavy" },
        { new List<int> { 95 }, "Thunderstorm: Slight or moderate" },
        { new List<int> { 96, 99 }, "Thunderstorm with slight and heavy hail" },
    };

    /// <summary>
    /// Get Description WMO Weather interpretation codes (WW)
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string GetWmo(int code)
    {
        var wmo = WmoCodes?.FirstOrDefault(x => x.Key.Contains(code));
        return wmo?.Value ?? "Unknown";  // default to unknown if code not found
    }
}
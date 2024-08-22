using System.Text.Json.Serialization;

namespace Gis.Net.OpenMeteo.AirQuality;

/// <summary>
/// Represents the hourly air quality measurements.
/// </summary>
public partial class Hourly
{
    /// <summary>
    /// Represents the Sulphur Dioxide property in the Hourly class.
    /// </summary>
    [JsonPropertyName("sulphur_dioxide")]
    public List<double?> SulphurDioxide { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index (Aqi) PM2.5 values.
    /// </summary>
    [JsonPropertyName("european_aqi_pm2_5")]
    public List<long?> EuropeanAqiPm25 { get; set; }

    /// <summary>
    /// Represents the Nitrogen Dioxide property in the air quality data.
    /// </summary>
    [JsonPropertyName("nitrogen_dioxide")]
    public List<double?> NitrogenDioxide { get; set; }

    /// <summary>
    /// Represents the hourly measurements of PM10 (particulate matter with a diameter of 10 micrometers or less) in the air quality data.
    /// </summary>
    /// <remarks>
    /// The PM10 property provides a list of hourly measurements of PM10 concentrations. The values are in micrograms per cubic meter (µg/m³).
    /// The PM10 is a type of air pollutant that consists of particles suspended in the air, which can contribute to respiratory and cardiovascular health issues when inhaled.
    /// The measurements are captured at different time intervals throughout the day.
    /// </remarks>
    /// <seealso cref="Hourly"/>
    [JsonPropertyName("pm10")]
    public List<double?> Pm10 { get; set; }

    /// <summary>
    /// Represents the list of ozone values.
    /// </summary>
    [JsonPropertyName("ozone")]
    public List<double?> Ozone { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index (AQI) for Ozone.
    /// </summary>
    [JsonPropertyName("european_aqi_ozone")]
    public List<long?> EuropeanAqiOzone { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index (AQI) for Nitrogen Dioxide.
    /// </summary>
    /// <remarks>
    /// The European AQI for Nitrogen Dioxide is a measure of air quality based on the concentration of Nitrogen Dioxide (NO2) in the air.
    /// This property provides a list of European AQI values for Nitrogen Dioxide over a given time period.
    /// </remarks>
    [JsonPropertyName("european_aqi_nitrogen_dioxide")]
    public List<long?> EuropeanAqiNitrogenDioxide { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index for PM10 pollutant.
    /// </summary>
    /// <remarks>
    /// The European Air Quality Index (EAQI) is an indicator of air quality based on the concentrations of different air pollutants, including PM10 (particulate matter with a diameter of less than 10 micrometers).
    /// PM10 can be harmful when inhaled as it can penetrate into the respiratory system and cause health problems.
    /// The EAQI for PM10 is calculated using a formula that takes into account the concentration of PM10 and the reference level set by the European Union.
    /// </remarks>
    [JsonPropertyName("european_aqi_pm10")]
    public List<long?> EuropeanAqiPm10 { get; set; }

    /// <summary>
    /// Represents the PM2.5 (Particulate Matter 2.5) property in the air quality data.
    /// </summary>
    [JsonPropertyName("pm2_5")]
    public List<double?> Pm25 { get; set; }

    /// <summary>
    /// Represents the European Air Quality Index for Sulphur Dioxide.
    /// </summary>
    [JsonPropertyName("european_aqi_sulphur_dioxide")]
    public List<long?> EuropeanAqiSulphurDioxide { get; set; }

    /// <summary>
    /// Represents the time property in the Hourly class.
    /// </summary>
    [JsonPropertyName("time")]
    public List<string?> Time { get; set; }

    /// <summary>
    /// Represents the measurements of carbon monoxide in the air.
    /// </summary>
    [JsonPropertyName("carbon_monoxide")]
    public List<double?> CarbonMonoxide { get; set; }
    
    /// <summary>
    /// Represents the European Air Quality Index (European AQI) property in the Hourly class.
    /// </summary>
    [JsonPropertyName("european_aqi")]
    public List<long?> EuropeanAqi { get; set; }
}
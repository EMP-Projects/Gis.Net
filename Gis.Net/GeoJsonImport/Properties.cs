using System.Text.Json.Serialization;

namespace Gis.Net.GeoJsonImport;

/// <summary>
/// Represents properties of a feature import object in GeoJSON format.
/// </summary>
public class Properties
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the OpId property.
    /// </summary>
    /// <value>
    /// The OpId property is used to store the OpId value.
    /// </value>
    [JsonPropertyName("op_id")]
    public string OpId { get; set; }

    /// <summary>
    /// Gets or sets the MinintElettorale property.
    /// </summary>
    [JsonPropertyName("minint_elettorale")]
    public string MinintElettorale { get; set; }

    /// <summary>
    /// Gets or sets the MinintFinloc property.
    /// </summary>
    /// <remarks>
    /// This property represents the MinintFinloc value of the feature.
    /// </remarks>
    [JsonPropertyName("minint_finloc")]
    public string MinintFinloc { get; set; }

    /// <summary>
    /// Gets or sets the province name.
    /// </summary>
    [JsonPropertyName("prov_name")]
    public string ProvName { get; set; }

    /// <summary>
    /// Gets or sets the ISTAT code of the province.
    /// </summary>
    [JsonPropertyName("prov_istat_code")]
    public string ProvIstatCode { get; set; }

    /// <summary>
    /// Gets or sets the numeric value of the prov_istat_code property.
    /// </summary>
    /// <remarks>
    /// This property represents the numeric value of the prov_istat_code property
    /// in the Properties class. It is used to store the ISTAT code for the province.
    /// </remarks>
    [JsonPropertyName("prov_istat_code_num")]
    public int? ProvIstatCodeNum { get; set; }

    /// <summary>
    /// Gets or sets the ProvAcr property.
    /// </summary>
    /// <remarks>
    /// The ProvAcr property represents the abbreviation code for the province.
    /// </remarks>
    [JsonPropertyName("prov_acr")]
    public string ProvAcr { get; set; }

    /// <summary>
    /// Gets or sets the region name.
    /// </summary>
    /// <value>
    /// The name of the region.
    /// </value>
    [JsonPropertyName("reg_name")]
    public string RegName { get; set; }

    /// <summary>
    /// Gets or sets the ISTAT code of the region.
    /// </summary>
    [JsonPropertyName("reg_istat_code")]
    public string RegIstatCode { get; set; }

    /// <summary>
    /// Represents the numeric ISTAT code for the region.
    /// </summary>
    [JsonPropertyName("reg_istat_code_num")]
    public int? RegIstatCodeNum { get; set; }

    /// <summary>
    /// Represents the OpdmId property.
    /// </summary>
    [JsonPropertyName("opdm_id")]
    public string OpdmId { get; set; }

    /// <summary>
    /// Represents the properties of a GeoJSON feature.
    /// </summary>
    [JsonPropertyName("com_catasto_code")]
    public string ComCatastoCode { get; set; }

    /// <summary>
    /// Represents the Istat code of a municipality.
    /// </summary>
    [JsonPropertyName("com_istat_code")]
    public string ComIstatCode { get; set; }

    /// <summary>
    /// Gets or sets the numeric ISTAT code for the municipality.
    /// </summary>
    [JsonPropertyName("com_istat_code_num")]
    public int? ComIstatCodeNum { get; set; }
}
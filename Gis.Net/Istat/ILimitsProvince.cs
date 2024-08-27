namespace Gis.Net.Istat;

/// <summary>
/// Represents a province with its limits and associated properties.
/// </summary>
public interface ILimitsProvince : ILimitsRegion
{
    /// <summary>
    /// Gets or sets the name of the province.
    /// </summary>
    string? ProvName { get; set; }

    /// <summary>
    /// Gets or sets the numeric ISTAT code of the province.
    /// </summary>
    int? ProvIstatCodeNum { get; set; }

    /// <summary>
    /// Gets or sets the acronym of the province.
    /// </summary>
    string? ProvAcr { get; set; }
    
    /// <summary>
    /// Gets or sets the ISTAT code of the province.
    /// </summary>
    string? ProvIstatCode { get; set; }
}
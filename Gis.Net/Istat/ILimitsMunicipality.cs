using NetTopologySuite.Geometries;

namespace Gis.Net.Istat;

/// <summary>
/// Represents a municipality with its limits and associated properties.
/// </summary>
public interface ILimitsMunicipality: ILimitsModelBase
{
    /// <summary>
    /// Gets or sets the name of the municipality.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the operational identifier of the municipality.
    /// </summary>
    string? OpId { get; set; }

    /// <summary>
    /// Gets or sets the German name of the municipality.
    /// </summary>
    string? NameDe { get; set; }

    /// <summary>
    /// Gets or sets the Slovenian name of the municipality.
    /// </summary>
    string? NameSl { get; set; }

    /// <summary>
    /// Gets or sets the electoral code of the municipality.
    /// </summary>
    string? MinintElettorale { get; set; }

    /// <summary>
    /// Gets or sets the financial code of the municipality.
    /// </summary>
    string? MinintFinloc { get; set; }

    /// <summary>
    /// Gets or sets the Italian name of the municipality.
    /// </summary>
    string? NameIt { get; set; }

    /// <summary>
    /// Gets or sets the cadastral code of the municipality.
    /// </summary>
    string? ComCatastoCode { get; set; }

    /// <summary>
    /// Gets or sets the ISTAT code of the municipality.
    /// </summary>
    string? ComIstatCode { get; set; }

    /// <summary>
    /// Gets or sets the numeric ISTAT code of the municipality.
    /// </summary>
    int? ComIstatCodeNum { get; set; }

    /// <summary>
    /// Gets or sets the geometry of the municipality in Well-Known Binary (WKB) format.
    /// </summary>
    MultiPolygon? WkbGeometry { get; set; }
    
    /// <summary>
    /// Gets or sets the operational domain identifier of the municipality.
    /// </summary>
    string? OpdmId { get; set; }
}
using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;
using NetTopologySuite.Geometries;

namespace Gis.Net.Vector.DTO;

/// <summary>
/// Represents a GIS Data Transfer Object (DTO).
/// </summary>
public class GisDto : DtoBase, IGisDto
{
    /// <summary>
    /// Represents a globally unique identifier (GUID).
    /// </summary>
    [JsonPropertyName("guid")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Represents the geometric property of a GIS data transfer object.
    /// </summary>
    [JsonPropertyName("geom"), JsonIgnore]
    public Geometry? Geom { get; set; }
}
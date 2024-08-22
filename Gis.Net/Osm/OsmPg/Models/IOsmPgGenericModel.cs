namespace Gis.Net.Osm.OsmPg.Models;

/// <summary>
/// Represents a generic model used in the OsmPg database.
/// </summary>
public interface IOsmPgGenericModel
{
    /// <summary>
    /// Gets or sets the ID of the property.
    /// </summary>
    long Id { get; set; }
}
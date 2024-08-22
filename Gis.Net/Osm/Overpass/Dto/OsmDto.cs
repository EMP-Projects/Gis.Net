using NetTopologySuite.Geometries;

namespace Gis.Net.Osm.Overpass.Dto;

/// <summary>
/// Represents a data transfer object for OpenStreetMap (OSM) elements.
/// </summary>
public class OsmDto
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a single OpenStreetMap (OSM) element.
    /// </summary>
    public OsmDto(Element element, Geometry geom, bool isPolygon = false)
    {
        Element = element;
        Geom = geom;
        IsPolygon = isPolygon;
    }

    /// <summary>
    /// Represents an element in the Overpass response.
    /// </summary>
    public Element Element { get; set; }

    /// <summary>
    /// Represents a property of a geometric entity.
    /// </summary>
    public Geometry Geom { get; set; }

    /// <summary>
    /// Represents the property that indicates whether a geometry is a polygon or not.
    /// </summary>
    public bool IsPolygon { get; set; }
}
using NetTopologySuite.Geometries;

namespace Gis.Net.Osm.Overpass.Dto;

public class OsmDto
{
    public OsmDto(Element element, Geometry geom, bool isPolygon = false)
    {
        Element = element;
        Geom = geom;
        IsPolygon = isPolygon;
    }

    public Element Element { get; set; }

    public Geometry Geom { get; set; }

    public bool IsPolygon { get; set; }
}
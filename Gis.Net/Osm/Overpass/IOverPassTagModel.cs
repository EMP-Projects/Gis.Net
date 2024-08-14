namespace Gis.Net.Osm.Overpass;

public interface IOverPassTagModel<T> where T : class
{
    long? QueryId { get; set; }
    T? Query { get; set; }
    string? Tag { get; set; }
    string? Description { get; set; }
}
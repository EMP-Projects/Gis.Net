using Gis.Net.Vector;

namespace Gis.Net.Wfs;

/// <summary>
/// Represents the options for a Web Feature Service (WFS) request.
/// </summary>
public class WfsOptions
{
    /// <summary>
    /// Initializes a new instance of the WfsOptions class with a single layer.
    /// </summary>
    /// <param name="layer">The name of the layer to include in the WFS request.</param>
    public WfsOptions(string layer)
    {
        Layer = layer;
    }
    
    /// <summary>
    /// Initializes a new instance of the WfsOptions class with multiple layers.
    /// </summary>
    /// <param name="layers">An array of layer names to include in the WFS request.</param>
    public WfsOptions(string[] layers)
    {
        Layers = layers;
        Layer = string.Join(",", layers);
    }
    
    /// <summary>
    /// Gets or sets the bounding box to define the spatial extent of the WFS request.
    /// </summary>
    /// <value>An array of strings representing the bounding box coordinates.</value>
    public string[]? BBox { get; set; }
    
    /// <summary>
    /// Gets or sets the single layer name for the WFS request.
    /// </summary>
    /// <value>The name of the layer.</value>
    public string? Layer { get; set; }
    
    /// <summary>
    /// Gets or sets the multiple layer names for the WFS request.
    /// </summary>
    /// <value>An array of layer names.</value>
    public string[]? Layers { get; set; }
    
    /// <summary>
    /// Gets or sets the version of the WFS service to use for the request.
    /// </summary>
    /// <value>The version string.</value>
    public string? Version { get; set; }
    
    /// <summary>
    /// Gets or sets the spatial reference system for the WFS request.
    /// </summary>
    /// <value>The SRS identifier, defaulting to "EPSG:3857".</value>
    public string? Srs { get; set; } = $"EPSG:{(int)ESrCode.WebMercator}";
}
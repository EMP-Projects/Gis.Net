namespace Gis.Net.Istat;

/// <summary>
/// Represents the base interface for limit models.
/// </summary>
public interface ILimitsModelBase
{
    /// <summary>
    /// Gets or sets the OGC FID (Object Identifier).
    /// </summary>
    int OgcFid { get; set; }
}
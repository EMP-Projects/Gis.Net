using NetTopologySuite.Features;

namespace Gis.Net.Wfs;

/// <summary>
/// Defines the contract for a service that interacts with a Web Feature Service (WFS).
/// </summary>
public interface IWfsService
{
    /// <summary>
    /// Asynchronously retrieves the capabilities document from the WFS service for a specific version.
    /// </summary>
    /// <param name="version">The version of the WFS service for which to retrieve capabilities.</param>
    /// <returns>A task representing the asynchronous operation, containing the capabilities XML as a string.</returns>
    Task<string> GetCapabilities(string version);

    /// <summary>
    /// Asynchronously retrieves the capabilities document from the WFS service using the default version.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, containing the capabilities XML as a string.</returns>
    Task<string> GetCapabilities();

    /// <summary>
    /// Asynchronously retrieves features from the WFS service based on the provided options.
    /// </summary>
    /// <param name="options">The options specifying the criteria for the feature request.</param>
    /// <returns>A task representing the asynchronous operation, containing the features XML as a string.</returns>
    Task<string> GetFeature(WfsOptions options);

    /// <summary>
    /// Asynchronously retrieves a collection of features from the WFS service based on the provided options.
    /// </summary>
    /// <param name="options">The options specifying the criteria for the feature request.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing a FeatureCollection object,
    /// or null if an error occurs.
    /// </returns>
    Task<FeatureCollection?> GetFeatureCollection(WfsOptions options);
}
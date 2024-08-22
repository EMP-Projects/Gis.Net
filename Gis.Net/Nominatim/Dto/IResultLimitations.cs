namespace Gis.Net.Nominatim.Dto;

/// <summary>
/// This class represents the limitations of a result when using the Nominatim service.
/// </summary>
public abstract class IResultLimitations
{
    /// <summary>
    /// Represents the zoom property for result limitations.
    /// </summary>
    public abstract int Zoom { get; set; }

    /// <summary>
    /// Represents the country codes used for result limitations in a search.
    /// </summary>
    public abstract List<string>? CountryCodes { get; set; }

    /// <summary>
    /// Represents the property to exclude specific place ids from the search results.
    /// </summary>
    /// <remarks>
    /// This property is used to exclude specific place ids from the search results.
    /// </remarks>
    /// <value>
    /// A nullable list of strings representing the place ids to be excluded from the search results.
    /// </value>
    public abstract List<string>? ExcludePlaceIds { get; set; }

    /// <summary>
    /// Represents the view box for limiting the search results in the Nominatim Search.
    /// </summary>
    public abstract List<double>? ViewBox { get; set; }

    /// <summary>
    /// Represents the result limitations for a search operation.
    /// </summary>
    public abstract int Bounded { get; set; }

    /// <summary>
    /// Represents the limitations for the search results.
    /// </summary>
    public abstract int Limit { get; set; }

    /// <summary>
    /// Represents an email property used for result limitations in Nominatim search.
    /// </summary>
    public abstract string? Email { get; set; }
}
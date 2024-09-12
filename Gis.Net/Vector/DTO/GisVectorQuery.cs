using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Vector.DTO;

/// <inheritdoc />
public abstract class GisVectorQuery : GisQuery
{
    /// <summary>
    /// Represents a geometric filter used for GIS queries.
    /// </summary>
    [FromQuery(Name = "geomFilter")] public string? GeomFilter { get; set; }

    /// <summary>
    /// Represents an array of property IDs used for GIS queries.
    /// </summary>
    [FromQuery(Name = "ids")]
    public long[]? Ids { get; set; }

    /// <summary>
    /// Represents the property ID used for a GIS vector query.
    /// </summary>
    [FromQuery(Name = "propertyId")] 
    public long? PropertyId { get; set; }

    /// <summary>
    /// Represents a property filter used for GIS queries.
    /// </summary>
    [FromQuery(Name = "propertiesIds")] 
    public long[]? PropertyIds { get; set; }

    /// <summary>
    /// Represents the distance for a GIS query.
    /// </summary>
    [FromQuery(Name = "distance")] 
    public long? Distance { get; set; }

    /// <summary>
    /// Gets a value indicating whether the GisVectorQuery object is valid.
    /// </summary>
    /// <remarks>
    /// The GisVectorQuery object is considered valid if it meets the following conditions:
    /// - The SrCode property is not null.
    /// - Either the GisGeometry property is null or both the SrCode and GisGeometry properties are not null.
    /// </remarks>
    public bool IsValid => this is { SrCode : not null, GisGeometry: null } || this is { SrCode : not null, GisGeometry: not null };
    
    /// <summary>
    /// Gets the error message if the GIS vector query is invalid.
    /// </summary>
    /// <remarks>
    /// The error message is determined based on the following conditions:
    /// - If the SrCode property is null, an error message indicating the need for the srCode parameter is returned.
    /// - If Measure is true and Buffer is null, an error message indicating the need for the Buffer parameter is returned.
    /// - If both LatY and LngX are less than or equal to 0, an error message indicating invalid geographic coordinates is returned.
    /// - If both LatY and LngX are greater than 0 and Distance is null or less than 0, an error message indicating the need for the Distance parameter is returned.
    /// - If any of LatYMin, LngXMin, LatYMax, or LngXMax are less than or equal to 0, an error message indicating invalid geographic coordinates is returned.
    /// - If LatYMin, LngXMin, LatYMax, and LngXMax are all greater than 0 and Distance is null, an error message indicating invalid geographic coordinates is returned.
    /// - If GeomFilter, LatY, LngX, LatYMin, LngXMin, LatYMax, and LngXMax are all null, an error message indicating the need for at least one geographical search criterion is returned.
    /// </remarks>
    public string? Error
    {
        get
        {
            return this switch
            {
                { SrCode: null } => "It is necessary to specify at least the srCode parameter for the geographic reference system [SrCode]",
                { Measure: true, Buffer: null } => "To calculate the measurements it is necessary to specify at least the Buffer parameter [Buffer]",
                { LatY: <= 0, LngX: <= 0 } => "Invalid values for X,Y geographic coordinates. [LatY, LngX]",
                { LatY: > 0, LngX: > 0, Distance: null or < 0 } => "To calculate the distance from a point you need the [Distance] parameter",
                { LatYMin: <= 0, LngXMin: <= 0, LatYMax: <= 0, LngXMax: <= 0 } => "Invalid values for geographic coordinates [LatYMin, LngXMin, LatYMax, LngXMax]",
                { LatYMin: > 0, LngXMin: > 0, LatYMax: > 0, LngXMax: > 0, Distance: null } => "Invalid values for geographic coordinates [LatYMin, LngXMin, LatYMax, LngXMax].",
                { GeomFilter: null, LatY: null, LngX: null, LatYMin: null, LngXMin: null, LatYMax: null, LngXMax: null } => "It is necessary to specify at least one geographical search criterion",
                _ => null
            };
        }
    }

    /// <summary>
    /// Represents a GIS geometry.
    /// </summary>
    public GisGeometry? GisGeometry
    {
        get
        {
            return this switch
            {
                { Error: null, GeomFilter: not null } => new GisGeometry((int)SrCode!, GeomFilter),
                { Error: null, LatY: <= 0, LngX: <= 0 } => null,
                { Error: null, LatYMin: <= 0, LngXMin: <= 0, LatYMax: <= 0, LngXMax: <= 0 } => null,
                { Error: null, LatY: > 0, LngX: > 0 } => new GisGeometry((int)SrCode!, (double)LatY, (double)LngX, (double)Distance!),
                { Error: null, LatYMin: > 0, LngXMin: > 0, LatYMax: > 0, LngXMax: > 0 } => new GisGeometry((int)SrCode!, (double)LngXMin, (double)LatYMin, (double)LngXMax, (double)LatYMax),
                _ => null
            };
        }
    }
}
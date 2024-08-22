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
    /// Represents the spatial reference code used for GIS data.
    /// </summary>
    [FromQuery(Name = "srCode")]
    public int? SrCode { get; set; } = (int)ESrCode.WebMercator;

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
    /// Represents an error message related to the <see cref="GisNet.Vector.DTO.Error"/> property.
    /// </summary>
    /// <value>
    /// The error message is a string that provides information about the error condition.
    /// </value>
    public string? Error
    {
        get
        {
            return this switch
            {
                { SrCode: null } => "E' necessario specificare almeno il parametro srCode per il sistema di riferimento geografico [SrCode]",
                { Measure: true, Buffer: null } => "Per calcolare le misure è' necessario specificare almeno il parametro Buffer [Buffer]",
                { LatY: <= 0, LngX: <= 0 } => "Valori non validi per le coordinate geografiche X,Y. [LatY, LngX]",
                { LatY: > 0, LngX: > 0, Distance: null or < 0 } => "Per calcolare la distanza da un punto è necessario il parametro [Distance]",
                { LatYMin: <= 0, LngXMin: <= 0, LatYMax: <= 0, LngXMax: <= 0 } => "Valori non validi per le coordinate geografiche [LatYMin, LngXMin, LatYMax, LngXMax].",
                { LatYMin: > 0, LngXMin: > 0, LatYMax: > 0, LngXMax: > 0, Distance: null } => "Valori non validi per le coordinate geografiche [LatYMin, LngXMin, LatYMax, LngXMax].",
                { GeomFilter: null, LatY: null, LngX: null, LatYMin: null, LngXMin: null, LatYMax: null, LngXMax: null } => "E' necessario specificare almeno un criterio di ricerca geografico.",
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
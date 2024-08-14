using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Vector.DTO;

/// <inheritdoc />
public abstract class GisVectorQuery : GisQuery
{
    [FromQuery(Name = "geomFilter")] public string? GeomFilter { get; set; }
    
    [FromQuery(Name = "srCode")]
    public int? SrCode { get; set; } = (int)ESrCode.WebMercator;
    
    /// <summary>
    /// Array di Id per filtrare i record dei vettori geografici direttamente con ID
    /// </summary>
    [FromQuery(Name = "ids")] 
    public long[]? Ids { get; set; }
    
    /// <summary>
    /// Id della proprietà per filtrare il record del vettore grafico 
    /// </summary>
    [FromQuery(Name = "propertyId")] 
    public long? PropertyId { get; set; }
    
    /// <summary>
    /// Array di Id delle proprietà per filtrare i record dei vettori geografici direttamente con ID
    /// </summary>
    [FromQuery(Name = "propertiesIds")] 
    public long[]? PropertyIds { get; set; }
    
    [FromQuery(Name = "distance")] 
    public long? Distance { get; set; }
    
    public bool IsValid => this is { SrCode : not null, GisGeometry: null } || this is { SrCode : not null, GisGeometry: not null };

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
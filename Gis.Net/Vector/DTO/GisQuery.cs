using Gis.Net.Core.DTO;
using Gis.Net.Spatial.DTO;
using Microsoft.AspNetCore.Mvc;
using TeamSviluppo.Gis.DTO;

namespace Gis.Net.Vector.DTO;

public class GisQuery : QueryBase, IGisQuery, IGisRequest
{
    [FromQuery(Name = "lngXMin")] public double? LngXMin { get; set; }
    
    [FromQuery(Name = "latXMin")] public double? LatYMin { get; set; }
    
    [FromQuery(Name = "lngXMax")] public double? LngXMax { get; set; }
    
    [FromQuery(Name = "latYMin")] public double? LatYMax { get; set; }
    
    [FromQuery(Name = "lngX")] public double? LngX { get; set; }
    
    [FromQuery(Name = "latY")] public double? LatY { get; set; }
    
    [FromQuery(Name = "measure")]
    public bool Measure { get; set; }
    
    [FromQuery(Name = "srCode")]
    public int? SrCode { get; set; }
    
    [FromQuery(Name = "buffer")]
    public double? Buffer { get; set; }
    
    [FromQuery(Name = "boundary")]
    public bool? Boundary { get; set; }
    
    [FromQuery(Name = "difference")]
    public bool? Difference { get; set; }
}
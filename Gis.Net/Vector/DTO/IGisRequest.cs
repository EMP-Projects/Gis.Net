namespace Gis.Net.Spatial.DTO;

public interface IGisRequest
{
    bool Measure { get; set; }

    int? SrCode { get; set; } 

    double? Buffer { get; set; } 
    
    bool? Boundary { get; set; }
    
    bool? Difference { get; set; }
}
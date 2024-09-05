using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Http;

namespace Gis.Net.Vector.DTO;

/// <summary>
/// Represents a GIS Vector Data Transfer Object (DTO).
/// </summary>
public abstract class GisVectorDto : GisDto
{
    /// <summary>
    /// Represents a GIS Data Transfer Object (DTO).
    /// </summary>
    [JsonPropertyName("file"), JsonIgnore] public IFormFile? File { get; set; }

    /// <summary>
    /// Represents a GIS Data Transfer Object (DTO).
    /// </summary>
    [JsonPropertyName("urlFile"), JsonIgnore] public string? UrlFile { get; set; }

    /// <summary>
    /// Represents the upload body for a GIS vector DTO.
    /// </summary>
    [JsonPropertyName("uploadBody"), JsonIgnore] public string? UploadBody { get; set; }
}

/// <summary>
/// Represents a GIS Vector Data Transfer Object (DTO).
/// </summary>
public abstract class GisVectorDto<T> : GisVectorDto, IGisVectorPropertiesDto<T> where T : DtoBase
{

    /// <summary>
    /// Represents the identification properties of a GIS vector entity.
    /// </summary>
    [JsonPropertyName("idProperties")] public long? IdProperties { get; set; }
    
    /// <summary>
    /// Represents the properties of a GIS vector object.
    /// </summary>
    [JsonPropertyName("properties")] public T? Properties { get; set; }
}
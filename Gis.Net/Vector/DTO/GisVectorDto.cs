using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Http;

namespace Gis.Net.Vector.DTO;

public abstract class GisVectorDto : GisDto
{
    [JsonPropertyName("file"), JsonIgnore] public IFormFile? File { get; set; }

    [JsonPropertyName("urlFile"), JsonIgnore] public string? UrlFile { get; set; }
    
    [JsonPropertyName("uploadBody"), JsonIgnore] public string? UploadBody { get; set; }
}

public abstract class GisVectorDto<T> : GisVectorDto, IGisVectorPropertiesDto<T> where T : DtoBase
{

    [JsonPropertyName("idProperties")] public long? IdProperties { get; set; }
    [JsonPropertyName("properties")] public T? Properties { get; set; }
}
using System.Text.Json.Serialization;

namespace Gis.Net.Aws.AWSCore.Cognito.Services;

/// <summary>
/// 
/// </summary>
public class AwsAuthUser
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="guid"></param>
    public AwsAuthUser(Guid guid) => Guid = guid;
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("guid")] public Guid? Guid { get; set; }
}
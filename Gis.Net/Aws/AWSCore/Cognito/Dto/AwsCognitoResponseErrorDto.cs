using Gis.Net.Aws.AWSCore.Dto;

namespace Gis.Net.Aws.AWSCore.Cognito.Dto;

/// <summary>
/// Represents a response error specific to AWS Cognito.
/// </summary>
public class AwsCognitoResponseErrorDto : AwsResponseErrorDto
{
    /// <summary>
    /// Represents a response error specific to AWS Cognito.
    /// </summary>
    public AwsCognitoResponseErrorDto(string message) : base(message)
    {
    }
}
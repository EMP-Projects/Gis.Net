namespace Gis.Net.Aws.AWSCore.SNS.Dto;

/// <summary>
/// 
/// </summary>
public class AwsConfirmDto : AwsSnsDto
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <param name="topicArn"></param>
    public AwsConfirmDto(string token, string topicArn)
    {
        Token = token;
        TopicArn = topicArn;
    }
}
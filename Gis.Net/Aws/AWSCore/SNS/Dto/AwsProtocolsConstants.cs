namespace Gis.Net.Aws.AWSCore.SNS.Dto;

/// <summary>
/// 
/// </summary>
public abstract class AwsProtocolsConstants
{
    /// <summary>
    /// delivery of JSON-encoded message to an Amazon SQS queue
    /// </summary>
    public const string Sqs = "sqs";                    
    
    /// <summary>
    /// delivery of message via SMTP
    /// </summary>
    public const string EMail = "email";                
    
    /// <summary>
    /// delivery of JSON-encoded message via SMTP
    /// </summary>
    public const string EMailJson = "email-json";    
    
    /// <summary>
    /// delivery of JSON-encoded message via HTTPS POST
    /// </summary>
    public const string Https = "https";                
    
    /// <summary>
    /// delivery of JSON-encoded message via HTTP POST
    /// </summary>
    public const string Http = "http";                  
    
    /// <summary>
    /// delivery of message via SMS
    /// </summary>
    public const string Sms = "sms";                    
        
    /// <summary>
    /// delivery of JSON-encoded message to an EndpointArn for a mobile app and device
    /// </summary>
    public const string Application = "application";    
    
    /// <summary>
    /// delivery of JSON-encoded message to an AWS Lambda function
    /// </summary>
    public const string Lambda = "lambda";              
    
    /// <summary>
    /// delivery of JSON-encoded message to an Amazon Kinesis Data Firehose delivery stream.
    /// </summary>
    public const string FireHose = "firehose";          
}
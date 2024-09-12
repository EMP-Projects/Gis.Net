using System.Net;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Gis.Net.Aws.AWSCore.Exceptions;
using Gis.Net.Aws.AWSCore.SNS.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Aws.AWSCore.SNS.Services;

/// <inheritdoc />
public class AwsSnsService : IAwsSnsService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AwsSnsService> _logger;
    private readonly IAmazonSimpleNotificationService _snsClient;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AwsSnsService"/> class.
    /// </summary>
    /// <param name="configuration">The configuration settings.</param>
    /// <param name="logger">The logger instance for logging.</param>
    /// <param name="snsClient">The Amazon SNS client instance.</param>
    public AwsSnsService(IConfiguration configuration, 
        ILogger<AwsSnsService> logger, 
        IAmazonSimpleNotificationService snsClient)
    {
        _configuration = configuration;
        _logger = logger;
        _snsClient = snsClient;
    }

    private string TopicArnDefault => _configuration["AWS_TOPIC_ARN"]!;

    /// <inheritdoc />
    public async Task<string> ConfirmSubscriptionAsync(AwsConfirmDto options, CancellationToken cancel)
    {
        var request = new ConfirmSubscriptionRequest
        {
            TopicArn = options.TopicArn,
            Token = options.Token
        };

        var confirmResponse = await _snsClient.ConfirmSubscriptionAsync(request, cancel);
        return confirmResponse.SubscriptionArn;
    }

    /// <summary>
    /// Subscribe a queue to a topic with optional filters.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<string> SubscribeTopic(AwsSubscribeDto options, CancellationToken cancel)
    {
        options.TopicArn ??= TopicArnDefault;

        if (string.IsNullOrEmpty(options.EndPoint))
            throw new Exception("Endpoint must be specified");

        var subscribeRequest = new SubscribeRequest
        {
            TopicArn = options.TopicArn,
            Protocol = options.Protocol,
            Endpoint = options.EndPoint,
            ReturnSubscriptionArn = true
        };

        if (!string.IsNullOrEmpty(options.FilterPolicy))
            subscribeRequest.Attributes = new Dictionary<string, string>
            {
                { "FilterPolicy", options.FilterPolicy }
            };

        var subscribeResponse = await _snsClient.SubscribeAsync(subscribeRequest, cancel);
        return subscribeResponse.SubscriptionArn;
    }

    /// <summary>
    /// Let's get a list of topics.
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> ListTopics(CancellationToken cancel)
    {
        var response = await _snsClient.ListTopicsAsync(new ListTopicsRequest(), cancel);
        return response.Topics.Select(topic => topic.TopicArn).ToList();
    }

    /// <summary>
    /// Checks to see if the supplied phone number has been opted out.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CheckIfOptedOut(AwsSubscribeCheckDto options, CancellationToken cancel)
    {
        var request = new CheckIfPhoneNumberIsOptedOutRequest
        {
            PhoneNumber = options.PhoneNumber
        };

        var response = await _snsClient.CheckIfPhoneNumberIsOptedOutAsync(request, cancel);
        return response.HttpStatusCode == HttpStatusCode.OK && response.IsOptedOut;
    }
    
    private static CreateTopicRequest TopicRequest(AwsTopicRequestDto request)
    {
        var createTopicRequest = new CreateTopicRequest
        {
            Name = request.UseFifoTopic && !request.TopicName!.EndsWith(".fifo") ? $"{request.TopicName}.fifo" : request.TopicName
        };

        if (!request.UseFifoTopic) return createTopicRequest;
        
        // Add the attributes from the method parameters.
        createTopicRequest.Attributes = new Dictionary<string, string>
        {
            { "FifoTopic", "true" }
        };

        if (request.UseContentBasedDeduplication)
            createTopicRequest.Attributes.Add("ContentBasedDeduplication", "true");

        return createTopicRequest;
    }

    /// <summary>
    /// Creates a new SNS topic using the supplied topic name.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<string> CreateTopic(AwsTopicRequestDto request, CancellationToken cancel)
    {
        var snsRequest = TopicRequest(request);
        var response = await _snsClient.CreateTopicAsync(snsRequest, cancel);
        return response.TopicArn;
    }

    /// <summary>
    /// Unsubscribe from a topic by a subscription ARN.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<bool> Unsubscribe(AwsUnSubscribeDto options, CancellationToken cancel)
    {
        var unsubscribeResponse = await _snsClient.UnsubscribeAsync(
            new UnsubscribeRequest
            {
                SubscriptionArn = options.SubscriptionArn
            }, cancel);

        return unsubscribeResponse.HttpStatusCode == HttpStatusCode.OK;
    }

    /// <summary>
    /// Delete a topic by its topic ARN.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<bool> DeleteTopic(AwsSnsDto? options, CancellationToken cancel)
    {
        options ??= new AwsSnsDto
        {
            TopicArn = TopicArnDefault
        };
        
        var deleteResponse = await _snsClient.DeleteTopicAsync(
            new DeleteTopicRequest
            {
                TopicArn = options.TopicArn
            }, cancel);
        return deleteResponse.HttpStatusCode == HttpStatusCode.OK;
    }

    /// <summary>
    /// Given the ARN of the Amazon SNS topic, this method retrieves the topic
    /// attributes.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetTopicAttributes(AwsSnsDto? options, CancellationToken cancel)
    {
        options ??= new AwsSnsDto
        {
            TopicArn = TopicArnDefault
        };
        
        var response = await _snsClient.GetTopicAttributesAsync(options.TopicArn, cancel);
        return response.Attributes;
    }

    /// <summary>
    /// Gets a list of the existing Amazon SNS subscriptions, optionally by specifying a topic ARN.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<List<Subscription>> GetSubscriptions(AwsSnsDto? options)
    {
        options ??= new AwsSnsDto
        {
            TopicArn = TopicArnDefault
        };
        
        var results = new List<Subscription>();

        if (!string.IsNullOrEmpty(options.TopicArn))
        {
            var paginateByTopic = _snsClient.Paginators.ListSubscriptionsByTopic(
                new ListSubscriptionsByTopicRequest
                {
                    TopicArn = options.TopicArn,
                });

            // Get the entire list using the paginator.
            await foreach (var subscription in paginateByTopic.Subscriptions)
            {
                results.Add(subscription);
            }
        }
        else
        {
            var paginateAllSubscriptions = _snsClient.Paginators.ListSubscriptions(new ListSubscriptionsRequest());

            // Get the entire list using the paginator.
            await foreach (var subscription in paginateAllSubscriptions.Subscriptions)
                results.Add(subscription);
        }

        return results;
    }

    /// <summary>
    /// Publish a message to a topic with an attribute and optional deduplication and group IDs.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<string> Publish(AwsPublishDto options, CancellationToken cancel)
    {
        var publishRequest = new PublishRequest
        {
            TopicArn = options.TopicArn,
            Message = options.Message,
            MessageDeduplicationId = options.DeDuplicationId,
            MessageGroupId = options.GroupId
        };

        if (options.AttributeValue != null)
        {
            // Add the string attribute if it exists.
            publishRequest.MessageAttributes =
                new Dictionary<string, MessageAttributeValue>
                {
                    {
                        options.AttributeName!,
                        new MessageAttributeValue { StringValue = options.AttributeValue, DataType = "String" }
                    }
                };
        }

        var publishResponse = await _snsClient.PublishAsync(publishRequest, cancel);
        return publishResponse.MessageId;
    }
}
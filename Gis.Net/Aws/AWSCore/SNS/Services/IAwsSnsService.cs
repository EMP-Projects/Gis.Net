using Amazon.SimpleNotificationService.Model;
using Gis.Net.Aws.AWSCore.SNS.Dto;

namespace Gis.Net.Aws.AWSCore.SNS.Services;

/// <summary>
/// Interface for AWS SNS (Simple Notification Service) operations.
/// </summary>
public interface IAwsSnsService
{
    /// <summary>
    /// Confirms a subscription asynchronously.
    /// </summary>
    /// <param name="options">The confirmation options.</param>
    /// <param name="cancel">Cancellation token.</param>
    /// <returns>The subscription ARN (Amazon Resource Name).</returns>
    Task<string> ConfirmSubscriptionAsync(AwsConfirmDto options, CancellationToken cancel);
    
    /// <summary>
    /// Subscribes to a topic asynchronously.
    /// </summary>
    /// <param name="options">The subscription options.</param>
    /// <param name="cancel">Cancellation token.</param>
    /// <returns>The subscription ARN (Amazon Resource Name).</returns>
    Task<string> SubscribeTopic(AwsSubscribeDto options, CancellationToken cancel);
    
    /// <summary>
    /// Lists topics asynchronously.
    /// </summary>
    /// <param name="cancel">Cancellation token.</param>
    /// <returns>A list of topic ARNs.</returns>
    Task<List<string>> ListTopics(CancellationToken cancel);

    /// <summary>
    /// Checks if a phone number is opted out asynchronously.
    /// </summary>
    /// <param name="options">The checking options.</param>
    /// <param name="cancel">Cancellation token.</param>
    /// <returns>True if the phone number is opted out, otherwise false.</returns>
    Task<bool> CheckIfOptedOut(AwsSubscribeCheckDto options, CancellationToken cancel);
    
    /// <summary>
    /// Creates a topic asynchronously.
    /// </summary>
    /// <param name="request">The topic creation request.</param>
    /// <param name="cancel">Cancellation token.</param>
    /// <returns>The created topic ARN (Amazon Resource Name).</returns>
    Task<string> CreateTopic(AwsTopicRequestDto request, CancellationToken cancel);
    
    /// <summary>
    /// Unsubscribes from a topic asynchronously.
    /// </summary>
    /// <param name="options">The unsubscription options.</param>
    /// <param name="cancel">Cancellation token.</param>
    /// <returns>True if unsubscription was successful, otherwise false.</returns>
    Task<bool> Unsubscribe(AwsUnSubscribeDto options, CancellationToken cancel);
    
    /// <summary>
    /// Deletes a topic asynchronously.
    /// </summary>
    /// <param name="options">The topic deletion options.</param>
    /// <param name="cancel">Cancellation token.</param>
    /// <returns>True if deletion was successful, otherwise false.</returns>
    Task<bool> DeleteTopic(AwsSnsDto options, CancellationToken cancel);

    /// <summary>
    /// Gets topic attributes asynchronously.
    /// </summary>
    /// <param name="options">The topic for which to get attributes.</param>
    /// <param name="cancel">Cancellation token.</param>
    /// <returns>A dictionary of topic attributes.</returns>
    Task<Dictionary<string, string>> GetTopicAttributes(AwsSnsDto options, CancellationToken cancel);

    /// <summary>
    /// Gets subscriptions for a topic asynchronously.
    /// </summary>
    /// <param name="options">The topic for which to get subscriptions.</param>
    /// <returns>A list of subscriptions.</returns>
    Task<List<Subscription>> GetSubscriptions(AwsSnsDto options);
    
    /// <summary>
    /// Publishes a message to a topic asynchronously.
    /// </summary>
    /// <param name="options">The publishing options.</param>
    /// <param name="cancel">Cancellation token.</param>
    /// <returns>The message ID of the published message.</returns>
    Task<string> Publish(AwsPublishDto options, CancellationToken cancel);
}
using Amazon.SimpleNotificationService;
using Gis.Net.Aws.AWSCore.SNS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Aws.AWSCore.SNS;

/// <summary>
/// Represents a service manager for AWS SNS (Simple Notification Service) operations.
/// </summary>
public static class AwsSnsServiceManager
{
    /// <summary>
    /// Adds AWS Simple Notification Service (SNS) to the application.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance.</param>
    /// <returns>The WebApplicationBuilder instance.</returns>
    public static WebApplicationBuilder AddAwsSns(this WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
        builder.Services.AddAWSService<IAmazonSimpleNotificationService>()
                        .AddTransient<IAwsSnsService, AwsSnsService>();
        return builder;
    }
}
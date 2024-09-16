using Amazon.S3;
using Gis.Net.Aws.AWSCore.S3.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Gis.Net.Aws.AWSCore.S3;

/// <summary>
/// Provides methods for managing AWS S3 buckets and files.
/// </summary>
public static class AwsBucketServiceManager
{
    /// <summary>
    /// Adds AWS S3 bucket services to the web application.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance.</param>
    /// <returns>The updated WebApplicationBuilder instance.</returns>
    public static WebApplicationBuilder AddAwsBucketS3(this WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
        builder.Services.AddAWSService<IAmazonS3>();
        builder.Services.AddScoped<IAwsBucketService, AwsBucketService>();
        return builder;
    }
}
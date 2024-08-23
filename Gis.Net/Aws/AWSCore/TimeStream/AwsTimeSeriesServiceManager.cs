using Amazon.TimestreamQuery;
using Amazon.TimestreamWrite;
using Gis.Net.Aws.AWSCore.TimeStream.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Aws.AWSCore.TimeStream;

/// <summary>
/// Class that provides methods to manage AWS TimeStream services.
/// </summary>
public static class AwsTimeSeriesServiceManager
{
    /// <summary>
    /// Extends the <see cref="WebApplicationBuilder"/> to add AWS TimeStream services.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance.</param>
    /// <returns>The updated WebApplicationBuilder instance.</returns>
    public static WebApplicationBuilder AddAwsTimeStream(this WebApplicationBuilder builder)
    {
        var configAws = builder.Configuration.GetAWSOptions();
        builder.Services.AddDefaultAWSOptions(configAws);
        builder.Services.AddAWSService<IAmazonTimestreamQuery>()
            .AddAWSService<IAmazonTimestreamWrite>()
            .AddTransient<IAwsTimeStreamDatabaseService, AwsTimeStreamDatabaseService>()
            .AddTransient<IAwsTimeStreamTableService, AwsTimeStreamTableService>()
            .AddTransient<IAwsTimeStreamQueryService, AwsTimeStreamQueryService>();
        return builder;
    }
}
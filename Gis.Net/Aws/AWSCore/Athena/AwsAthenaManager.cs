using Amazon.Athena;
using Gis.Net.Aws.AWSCore.Athena.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Aws.AWSCore.Athena;

/// <summary>
/// Provides management functionality for AWS Athena.
/// </summary>
public static class AwsAthenaManager
{
    /// <summary>
    /// Adds AWS Athena services to the application.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance.</param>
    /// <returns>The updated WebApplicationBuilder instance.</returns>
    public static WebApplicationBuilder AddAwsAthena(this WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
        builder.Services.AddAWSService<IAmazonAthena>()
                        .AddScoped<IAwsAthenaService, AwsAthenaService>();
        return builder;
    }
}
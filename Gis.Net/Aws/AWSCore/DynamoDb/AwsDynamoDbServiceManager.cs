using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Net.Aws.AWSCore.DynamoDb;

/// <summary>
/// Provides methods to add AWS DynamoDB services to the application.
/// </summary>
public static class AwsDynamoDbServiceManager
{
    /// <summary>
    /// Adds DynamoDB services to the specified <see cref="WebApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to add services to.</param>
    /// <returns>The <see cref="WebApplicationBuilder"/> with DynamoDB services added.</returns>
    public static WebApplicationBuilder AddAwsDynamoDb(this WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
        builder.Services.AddAWSService<IAmazonDynamoDB>();
        builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
        return builder;
    }
}
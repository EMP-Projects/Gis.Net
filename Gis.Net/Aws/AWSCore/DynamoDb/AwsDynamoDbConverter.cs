using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Gis.Net.Vector;

namespace Gis.Net.Aws.AWSCore.DynamoDb;

public class AwsDynamoDbConverter<T> : IPropertyConverter where T : class
{
    /// <inheritdoc />
    public DynamoDBEntry ToEntry(object value)
    {
        return GisUtility.SerializeObject(value as T);
    }

    /// <inheritdoc />
    public object FromEntry(DynamoDBEntry entry)
    {
        return GisUtility.DeserializeObject<T>(entry.AsString());
    }
}
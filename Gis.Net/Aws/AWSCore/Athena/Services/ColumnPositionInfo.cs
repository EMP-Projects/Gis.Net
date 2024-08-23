using Amazon.Athena.Model;

namespace Gis.Net.Aws.AWSCore.Athena.Services;

/// <summary>
/// Represents information about the position and metadata of a column in a query result.
/// </summary>
public class ColumnPositionInfo
{
    /// <summary>
    /// Gets or sets the index position of a column in the first row.
    /// </summary>
    public int IndexPosition { get; set; }
    /// <summary>
    /// Represents column information used for mapping columns in Athena query results.
    /// </summary>
    public ColumnInfo? ColumnInfo { get; set; }
}
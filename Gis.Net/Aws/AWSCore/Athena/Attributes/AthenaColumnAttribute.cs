namespace Gis.Net.Aws.AWSCore.Athena.Attributes;

[AttributeUsage(AttributeTargets.Property)]
sealed class AthenaColumnAttribute : Attribute
{
    /// <summary>
    /// Map Amazon Athena column name to a proprierty
    /// </summary>
    /// <param name="columnName">Column name on Amazon Athena</param>
    public AthenaColumnAttribute(string columnName)
    {
        ColumnName = columnName.ToLower();
    }

    public string ColumnName { get; }

}
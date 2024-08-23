namespace Gis.Net.Aws.AWSCore.Cognito.Policies;

/// <summary>
/// Represents a class for attribute policy.
/// </summary>
public class AttributePolicy
{
    /// <summary>
    /// Represents a policy attribute.
    /// </summary>
    public AttributePolicy(string name, string value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    /// Represents the name of an attribute in an attribute policy.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Represents a class for attribute policy.
    /// </summary>
    public string Value { get; set; }
}
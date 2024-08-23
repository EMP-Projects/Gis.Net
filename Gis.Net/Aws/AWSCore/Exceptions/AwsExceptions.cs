namespace Gis.Net.Aws.AWSCore.Exceptions;

/// <summary>
/// The `AwsExceptions` class represents an exception specific to AWS operations.
/// </summary>
public class AwsExceptions : Exception
{

    /// <summary>
    /// The `AwsExceptions` class represents an exception specific to AWS operations.
    /// </summary>
    public AwsExceptions(string message)
        : base(message)
    {
    }
}
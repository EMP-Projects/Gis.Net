namespace Gis.Net.Core.Exceptions;

/// <summary>
/// The exception that is thrown when model validation fails.
/// </summary>
public class ModelValidationException : Exception
{
    /// ModelValidationException is an exception class that represents model validation errors.
    /// @namespace Gis.Net.Core.Exceptions
    /// /
    public List<string> Errors { get; private set; }

    /// <summary>
    /// Represents an exception that is thrown when model validation fails.
    /// </summary>
    /// <remarks>
    /// This exception is thrown when validation errors occur during model validation.
    /// </remarks>
    public ModelValidationException(List<string> errors) : base($"{errors.Count} validation errors occurred") => Errors = errors;

    /// <summary>
    /// Represents an exception that occurred during model validation.
    /// </summary>
    /// <remarks>
    /// This exception is thrown when validation errors occur during model validation.
    /// It provides access to the collection of validation errors that occurred.
    /// </remarks>
    public ModelValidationException(params string[] errors) : base($"{errors.Length} validation errors occurred") => Errors = errors.ToList();
}
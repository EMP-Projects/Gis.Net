namespace Gis.Net.Core.Exceptions;

public class ModelValidationException : Exception
{
    public IEnumerable<string> Errors { get; private set; }

    public ModelValidationException(IEnumerable<string> errors)
        : base($"Si sono verificati {errors.Count()} errori di validazione")
    {
        this.Errors = errors;
    }

    public ModelValidationException(params string[] errors)
        : base($"Si sono verificati {errors.Length} errori di validazione")
    {
        this.Errors = errors;
    }
}
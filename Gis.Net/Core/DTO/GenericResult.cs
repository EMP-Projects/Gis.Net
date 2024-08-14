namespace Gis.Net.Core.DTO;

public class GenericResult
{
    public GenericResult()
    {
        Result = new Dictionary<string, object>();
    }

    public GenericResult(Dictionary<string, object> result)
    {
        Result = result;
    }

    public Dictionary<string, object> Result { get; set; }
}
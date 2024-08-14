namespace Gis.Net.Core.DTO;

public class SingleResult<T> : ResultBase where T : IDtoBase
{
    public T Data { get; set; }
    public SingleResult(string error) => Error = error;
    public SingleResult(T data) => Data = data;
}
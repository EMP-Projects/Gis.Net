namespace Gis.Net.Core.DTO;

public interface IDtoBase
{
    long Id { get; set; }
    string Key { get; set; }
    DateTime TimeStamp { get; set; }
}
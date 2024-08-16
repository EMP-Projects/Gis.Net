namespace Gis.Net.Core.Entities;

public interface IModelBase
{
    long Id { get; set; }
    string Key { get; set; }
    DateTime TimeStamp { get; set; }
}
namespace Gis.Net.Core.Entities;

public interface IModelBase
{
    int Id { get; set; }
    string Key { get; set; }
    DateTime TimeStamp { get; set; }
}
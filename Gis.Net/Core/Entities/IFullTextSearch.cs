using NpgsqlTypes;

namespace Gis.Net.Core.Entities;

public interface IFullTextSearch
{
    NpgsqlTsVector? SearchText { get; set; }
}
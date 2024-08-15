using NpgsqlTypes;

namespace Gis.Net.Vector.Models;

public interface IGisCoreFullText
{
    NpgsqlTsVector? SearchText { get; set; }
}
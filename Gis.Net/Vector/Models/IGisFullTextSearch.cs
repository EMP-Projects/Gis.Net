using NpgsqlTypes;

namespace TeamSviluppo.Gis.NetCoreFw.Models;

public interface IGisCoreFullText
{
    NpgsqlTsVector? SearchText { get; set; }
}
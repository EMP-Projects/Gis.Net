using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Vector.Models;
using Gis.Net.VectorCore.OsmPg.Properties;
using TeamSviluppo.Gis.NetCoreFw.Models;
using Gis.Net.OsmPg.Properties;
namespace Gis.Net.OsmPg.Vector;

[Table("osm_vector")]
public class OsmVectorModel : GisCoreModel<OsmPropertiesModel>
{
    
}
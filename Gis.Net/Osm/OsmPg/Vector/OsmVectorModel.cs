using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Vector.Models;
using Gis.Net.Osm.OsmPg.Properties;
namespace Gis.Net.Osm.OsmPg.Vector;

[Table("osm_vector")]
public class OsmVectorModel : GisCoreModel<OsmPropertiesModel>
{
    
}
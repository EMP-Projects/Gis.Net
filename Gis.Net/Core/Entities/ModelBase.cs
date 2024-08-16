using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace Gis.Net.Core.Entities;

[Index(nameof(Key))]
public abstract class ModelBase : IModelBase, IFullTextSearch
{
    [Column("id"), Key]
    public long Id { get; set; }
    
    [Column("key")]
    public required string Key { get; set; }
    
    [Column("search_text")]
    public NpgsqlTsVector? SearchText { get; set; }
    
    [Column("timestamp")]
    public required DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}
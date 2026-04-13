using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Infrastructure.Data.Entities;

[Table("locations")]
public class LocationEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [StringLength(24)]
    public required string City { get; set; }
    
    [StringLength(2)]
    public required string Country { get; set; }

    public bool IsAvailable { get; set; } = true;
}
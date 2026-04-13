using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Infrastructure.Data.Entities;

[Table("rates")]
public class RateEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(24)]
    [Required]
    public string Name { get; set; } = null!;
    
    [DataType(DataType.Currency)]
    public Decimal Cost { get; set; }

    public uint VpnLevel { get; set; } = 0;
    
    public bool IsPrivate { get; set; } = false;
}
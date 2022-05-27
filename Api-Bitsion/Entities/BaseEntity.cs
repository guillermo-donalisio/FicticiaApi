using System.ComponentModel.DataAnnotations;

namespace Api_Bitsion.Entities;

public class BaseEntity
{
    [Key]
    public int ID {set;get;}
    public bool IsActive {set;get;}
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

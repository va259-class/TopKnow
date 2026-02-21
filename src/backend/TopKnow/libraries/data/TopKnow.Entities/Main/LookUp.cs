using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Entities.Abstractions;

namespace TopKnow.Entities.Main;

[Table("LookUps", Schema = "Main")]
public class LookUp : EntityBase
{
    [Required]
    [MaxLength(16)]
    public string Name { get; set; }

    public int Order { get; set; }

    [Required]
    public Guid TypeId { get; set; }

    [ForeignKey(nameof(TypeId))]
    public LookUpType LookUpType { get; set; }
}

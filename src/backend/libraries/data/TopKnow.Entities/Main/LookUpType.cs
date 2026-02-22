using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Entities.Abstractions;

namespace TopKnow.Entities.Main;

[Table("LookUpTypes", Schema = "Main")]
public class LookUpType : EntityBase
{
    [Required]
    [MaxLength(16)]
    public string Name { get; set; }
}

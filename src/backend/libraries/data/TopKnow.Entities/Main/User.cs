using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Entities.Abstractions;

namespace TopKnow.Entities.Main;

[Table("Users", Schema = "Main")]
public class User : EntityBase
{
    [MaxLength(32)]
    [Required]
    public string Mail { get; set; }
    [MaxLength(64)]
    [Required]
    public string Password { get; set; }
    [MaxLength(32)]
    [Required]
    public string DisplayName { get; set; }
}
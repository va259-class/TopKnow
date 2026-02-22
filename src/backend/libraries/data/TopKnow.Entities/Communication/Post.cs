using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Entities.Abstractions;
using TopKnow.Entities.Main;

namespace TopKnow.Entities.Communication;

[Table("Posts", Schema = "Communication")]
public class Post : EntityBase
{
    [Required]
    [MaxLength(64)]
    [MinLength(8)]
    public string Title { get; set; }

    [Required]
    [MaxLength(1024)]
    [MinLength(16)]
    public string Content { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}

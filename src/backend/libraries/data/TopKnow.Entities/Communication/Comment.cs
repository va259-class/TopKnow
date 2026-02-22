using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Entities.Abstractions;
using TopKnow.Entities.Main;

namespace TopKnow.Entities.Communication;

[Table("Comments", Schema = "Communication")]
public class Comment : EntityBase
{
    [Required]
    [MaxLength(256)]
    public string Content { get; set; }
    [Required]
    public Guid PostId { get; set; }
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}

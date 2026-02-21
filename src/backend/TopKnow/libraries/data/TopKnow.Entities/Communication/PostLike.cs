using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Common.Enums;
using TopKnow.Entities.Abstractions;
using TopKnow.Entities.Main;

namespace TopKnow.Entities.Communication;

[Table("PostLikes", Schema = "Communication")]
public class PostLike : EntityBase
{
    [Required]
    public Guid PostId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public LikeType Type { get; set; }

    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}

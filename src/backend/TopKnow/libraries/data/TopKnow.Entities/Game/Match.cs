using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Entities.Abstractions;
using TopKnow.Entities.Main;

namespace TopKnow.Entities.Game;

[Table("Matches", Schema = "Game")]
public class Match : EntityBase
{
    [Required]
    public Guid LeftUserId { get; set; }
    [Required]
    public Guid RightUserId { get; set; }
    [Required]
    public int RoundCount { get; set; }
    public Guid? WinnerUserId { get; set; }

    [ForeignKey(nameof(LeftUserId))]
    public User LeftUser { get; set; }

    [ForeignKey(nameof(RightUserId))]
    public User RightUser { get; set; }
    
    [ForeignKey(nameof(WinnerUserId))]
    public User WinnerUser { get; set; }
}



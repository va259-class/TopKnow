using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Entities.Abstractions;
using TopKnow.Entities.Main;

namespace TopKnow.Entities.Game;

[Table("Matches", Schema = "Game")]
public class Match : EntityBase
{
    [Required]
    public Guid LeftPlayerId { get; set; }
    [Required]
    public Guid RightPlayerId { get; set; }
    [Required]
    public int RoundCount { get; set; }
    public Guid? WinnerPlayerId { get; set; }

    [ForeignKey(nameof(LeftPlayerId))]
    public Player LeftPlayer { get; set; }

    [ForeignKey(nameof(RightPlayerId))]
    public Player RightPlayer { get; set; }
    
    [ForeignKey(nameof(WinnerPlayerId))]
    public Player WinnerPlayer { get; set; }
}



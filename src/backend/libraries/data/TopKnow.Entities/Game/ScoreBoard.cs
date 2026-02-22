using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Entities.Abstractions;
using TopKnow.Entities.Main;

namespace TopKnow.Entities.Game;

[Table("ScoreBoard", Schema = "Game")]
public class ScoreBoard : EntityBase
{
    [Required]
    public Guid PlayerId { get; set; }
    public int Score { get; set; }
    [Required]
    public Guid BoardTypeId { get; set; }

    [ForeignKey(nameof(PlayerId))]
    public Player User { get; set; }

    [ForeignKey(nameof(BoardTypeId))]
    public LookUp BoardType { get; set; }
}
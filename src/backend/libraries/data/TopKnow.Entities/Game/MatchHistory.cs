using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Entities.Abstractions;
using TopKnow.Entities.Main;

namespace TopKnow.Entities.Game;

[Table("MatchHistories", Schema = "Game")]
public class MatchHistory : EntityBase
{
    public Guid MatchId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid? WinnerPlayerId { get; set; }

    [ForeignKey(nameof(MatchId))]
    public Match Match { get; set; }

    [ForeignKey(nameof(QuestionId))]
    public Question Question { get; set; }

    [ForeignKey(nameof(WinnerPlayerId))]
    public Player WinnerUser { get; set; }
}



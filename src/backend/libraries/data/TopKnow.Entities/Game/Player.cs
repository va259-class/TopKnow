using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Entities.Abstractions;
using TopKnow.Entities.Main;

namespace TopKnow.Entities.Game;

[Table("Players", Schema = "Game")]
public class Player : EntityBase
{
    public string NickName { get; set; }
    public Guid UserId { get; set; }
    public int GameCount { get; set; }
    public int WinCount { get; set; }
    public int Strike { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}
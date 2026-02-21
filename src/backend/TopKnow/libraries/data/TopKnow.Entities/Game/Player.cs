using TopKnow.Entities.Abstractions;

namespace TopKnow.Entities.Game;

public class Player : EntityBase
{
    public string NickName { get; set; }
    public Guid UserId { get; set; }
    public int GameCount { get; set; }
    public int WinCount { get; set; }
    public int Strike { get; set; }
}
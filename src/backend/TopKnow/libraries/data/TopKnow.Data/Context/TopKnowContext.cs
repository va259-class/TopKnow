using Microsoft.EntityFrameworkCore;
using TopKnow.Entities.Communication;
using TopKnow.Entities.Game;
using TopKnow.Entities.Main;

namespace TopKnow.Data.Context;

public class TopKnowContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<LookUp> LookUps { get; set; }
    public DbSet<LookUpType> LookUpTypes { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<ScoreBoard> ScoreBoards { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }
}

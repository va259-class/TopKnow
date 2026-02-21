using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TopKnow.Entities.Communication;
using TopKnow.Entities.Game;
using TopKnow.Entities.Main;

namespace TopKnow.Data.Context;

public class TopKnowContext : DbContext
{
    public TopKnowContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        //Question içindeki cevapların listeden json array'e dönüşmesi için
        modelBuilder.Entity<Question>().Property(q => q.Answers)
                                       .HasConversion(v => JsonSerializer.Serialize(v, jsonOptions),
                                                      v => JsonSerializer.Deserialize<List<Answer>>(v, jsonOptions))
                                       .HasColumnType("nvarchar(512)");

        var foreignKeys = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
        foreach (var relationship in foreignKeys)
        {
            relationship.DeleteBehavior = DeleteBehavior.NoAction;
        }
    }

    public DbSet<User> Users { get; set; }
    public DbSet<LookUp> LookUps { get; set; }
    public DbSet<LookUpType> LookUpTypes { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<MatchHistory> History { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<ScoreBoard> ScoreBoards { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }
}

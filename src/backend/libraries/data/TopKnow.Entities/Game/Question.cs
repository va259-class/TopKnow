using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopKnow.Entities.Abstractions;
using TopKnow.Entities.Main;

namespace TopKnow.Entities.Game;

[Table("Questions", Schema = "Game")]
public class Question : EntityBase
{
    public Question()
    {
        Answers = new List<Answer>(4);
    }
    [Required]
    [MaxLength(128)]
    public string Title { get; set; }
    public List<Answer> Answers { get; set; }
    public Guid TypeId { get; set; }

    [ForeignKey(nameof(TypeId))]
    public LookUp Type { get; set; }
}

public class Answer
{
    public string Title { get; set; }
    public bool IsCorrect { get; set; }
}
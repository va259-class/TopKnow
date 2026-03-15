using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TopKnow.Data.Context;
using TopKnow.Entities.Game;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.Management.Commands.Questions;

public class AnswerInput
{
    public string Title { get; set; } = "";
    public bool IsCorrect { get; set; }
}

public class CreateQuestionRequestInput
{
    [Required]
    [MaxLength(128)]
    public string Title { get; set; } = "";

    public Guid TypeId { get; set; }

    public List<AnswerInput> Answers { get; set; } = new();
}

public class CreateQuestionRequest : IRequest<Result<Guid>>
{
    public CreateQuestionRequest(CreateQuestionRequestInput input)
    {
        Input = input;
    }

    public CreateQuestionRequestInput Input { get; }
}

internal class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionRequest, Result<Guid>>
{
    private readonly TopKnowContext context;

    public CreateQuestionCommandHandler(TopKnowContext context)
    {
        this.context = context;
    }

    public async Task<Result<Guid>> Handle(CreateQuestionRequest request, CancellationToken cancellationToken)
    {
        var typeExists = await context.LookUps.AnyAsync(l => l.Id == request.Input.TypeId && !l.IsDeleted, cancellationToken);
        if (!typeExists)
        {
            return Result<Guid>.Failure(new Error(ErrorCodes.INVALID_PARAMETER, "Geçersiz soru tipi"));
        }
        if (request.Input.Answers == null || request.Input.Answers.Count < 2 || request.Input.Answers.Count > 6)
        {
            return Result<Guid>.Failure(new Error(ErrorCodes.INVALID_PARAMETER, "2 ile 6 arasında cevap olmalıdır"));
        }
        var answers = request.Input.Answers.Where(a => !string.IsNullOrWhiteSpace(a.Title))
                                           .Select(a => new Answer { Title = a.Title.Trim(), IsCorrect = a.IsCorrect })
                                           .ToList();

        if (answers.Count < 2)
        {
            return Result<Guid>.Failure(new Error(ErrorCodes.INVALID_PARAMETER, "En az 2 dolu cevap giriniz"));
        }
        var id = Guid.NewGuid();
        var question = new Question
        {
            Id = id,
            Title = request.Input.Title.Trim(),
            TypeId = request.Input.TypeId,
            Answers = answers
        };

        context.Questions.Add(question);
        await context.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(id);
    }
}

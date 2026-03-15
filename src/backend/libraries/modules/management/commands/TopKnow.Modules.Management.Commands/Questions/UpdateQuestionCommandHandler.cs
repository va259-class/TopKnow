using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TopKnow.Data.Context;
using TopKnow.Entities.Game;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.Management.Commands.Questions;

public class UpdateQuestionRequestInput
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Title { get; set; } = "";

    public Guid TypeId { get; set; }

    public List<AnswerInput> Answers { get; set; } = new();
}

public class UpdateQuestionRequest : IRequest<Result<bool>>
{
    public UpdateQuestionRequest(UpdateQuestionRequestInput input)
    {
        Input = input;
    }

    public UpdateQuestionRequestInput Input { get; }
}

internal class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionRequest, Result<bool>>
{
    private readonly TopKnowContext _context;

    public UpdateQuestionCommandHandler(TopKnowContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(UpdateQuestionRequest request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions
            .FirstOrDefaultAsync(q => q.Id == request.Input.Id && !q.IsDeleted, cancellationToken);

        if (question == null)
            return Result<bool>.Failure(new Error(ErrorCodes.NOT_FOUND, "Soru bulunamadı"));

        var typeExists = await _context.LookUps.AnyAsync(l => l.Id == request.Input.TypeId && !l.IsDeleted, cancellationToken);
        if (!typeExists)
            return Result<bool>.Failure(new Error(ErrorCodes.INVALID_PARAMETER, "Geçersiz soru tipi"));

        if (request.Input.Answers == null || request.Input.Answers.Count < 2 || request.Input.Answers.Count > 6)
            return Result<bool>.Failure(new Error(ErrorCodes.INVALID_PARAMETER, "2 ile 6 arasında cevap olmalıdır"));

        var answers = request.Input.Answers
            .Where(a => !string.IsNullOrWhiteSpace(a.Title))
            .Select(a => new Answer { Title = a.Title.Trim(), IsCorrect = a.IsCorrect })
            .ToList();

        if (answers.Count < 2)
            return Result<bool>.Failure(new Error(ErrorCodes.INVALID_PARAMETER, "En az 2 dolu cevap giriniz"));

        question.Title = request.Input.Title.Trim();
        question.TypeId = request.Input.TypeId;
        question.Answers = answers;
        question.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Success(true);
    }
}

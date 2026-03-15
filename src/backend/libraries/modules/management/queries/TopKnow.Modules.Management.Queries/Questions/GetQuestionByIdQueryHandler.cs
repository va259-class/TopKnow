using MediatR;
using Microsoft.EntityFrameworkCore;
using TopKnow.Data.Context;
using TopKnow.Entities.Game;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.Management.Queries.Questions;

public record AnswerItemOutput(string Title, bool IsCorrect);

public record QuestionDetailOutput(Guid Id, string Title, Guid TypeId, List<AnswerItemOutput> Answers);

public class GetQuestionByIdRequest : IRequest<Result<QuestionDetailOutput>>
{
    public GetQuestionByIdRequest(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

internal class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdRequest, Result<QuestionDetailOutput>>
{
    private readonly TopKnowContext _context;

    public GetQuestionByIdQueryHandler(TopKnowContext context)
    {
        _context = context;
    }

    public async Task<Result<QuestionDetailOutput>> Handle(GetQuestionByIdRequest request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions
            .AsNoTracking()
            .Include(q => q.Type)
            .FirstOrDefaultAsync(q => q.Id == request.Id && !q.IsDeleted, cancellationToken);

        if (question == null)
            return Result<QuestionDetailOutput>.Failure(new Error(ErrorCodes.NOT_FOUND, "Soru bulunamadı"));

        var answers = question.Answers?
            .Select(a => new AnswerItemOutput(a.Title, a.IsCorrect))
            .ToList() ?? new List<AnswerItemOutput>();

        return Result<QuestionDetailOutput>.Success(
            new QuestionDetailOutput(question.Id, question.Title, question.TypeId, answers));
    }
}

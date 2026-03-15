using MediatR;
using Microsoft.EntityFrameworkCore;
using TopKnow.Data.Context;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.PlayGround.Queries.Questions;

// Using the structure expected by the frontend
public record PlayGroundQuestionOutput(Guid Id, string Question, List<string> Answers, int CorrectIndex);

public class GetRandomQuestionsRequest : IRequest<Result<List<PlayGroundQuestionOutput>>>
{
    public GetRandomQuestionsRequest(int count = 5)
    {
        Count = count;
    }

    public int Count { get; }
}

internal class GetRandomQuestionsQueryHandler : IRequestHandler<GetRandomQuestionsRequest, Result<List<PlayGroundQuestionOutput>>>
{
    private readonly TopKnowContext context;

    public GetRandomQuestionsQueryHandler(TopKnowContext context)
    {
        this.context = context;
    }

    public async Task<Result<List<PlayGroundQuestionOutput>>> Handle(GetRandomQuestionsRequest request, CancellationToken cancellationToken)
    {
        var questions = await context.Questions.AsNoTracking()
                                               .Where(q => !q.IsDeleted)
                                               .OrderBy(q => Guid.NewGuid())
                                               .Take(request.Count)
                                               .ToListAsync(cancellationToken);

        var output = new List<PlayGroundQuestionOutput>();

        foreach (var q in questions)
        {
            var answers = q.Answers ?? new List<TopKnow.Entities.Game.Answer>();
            
            var answerStrings = answers.Select(a => a.Title).ToList();
            int correctIndex = answers.FindIndex(a => a.IsCorrect);
            
            if (correctIndex == -1) correctIndex = 0;

            output.Add(new PlayGroundQuestionOutput(q.Id, q.Title, answerStrings, correctIndex));
        }

        return Result<List<PlayGroundQuestionOutput>>.Success(output);
    }
}

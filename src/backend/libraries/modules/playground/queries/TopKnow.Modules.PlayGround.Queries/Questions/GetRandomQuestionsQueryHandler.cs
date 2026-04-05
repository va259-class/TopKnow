using MediatR;
using Microsoft.EntityFrameworkCore;
using TopKnow.Data.Context;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.PlayGround.Queries.Questions;

public record PlayGroundQuestionOutput(Guid LeftUserId, Guid RightUserId, Guid QuestionId);

public class GetRandomQuestionsRequest : IRequest<Result<PlayGroundQuestionOutput>>
{
    public GetRandomQuestionsRequest(Guid matchId)
    {
        MatchId = matchId;
    }

    public Guid MatchId { get; }
}

internal class GetRandomQuestionsQueryHandler : IRequestHandler<GetRandomQuestionsRequest, Result<PlayGroundQuestionOutput>>
{
    private readonly TopKnowContext context;

    public GetRandomQuestionsQueryHandler(TopKnowContext context)
    {
        this.context = context;
    }

    public async Task<Result<PlayGroundQuestionOutput>> Handle(GetRandomQuestionsRequest request, CancellationToken cancellationToken)
    {
        var question = await context.Questions.AsNoTracking()
                                              .Where(q => !q.IsDeleted)
                                              .OrderBy(q => Guid.NewGuid())
                                              .FirstOrDefaultAsync(cancellationToken);

        var match = await context.Matches
                                 .AsNoTracking()     
                                 .Include(i => i.LeftPlayer)
                                 .Include(i => i.RightPlayer)
                                 .FirstOrDefaultAsync(f => f.Id == request.MatchId, cancellationToken);

        return Result<PlayGroundQuestionOutput>.Success(
            new PlayGroundQuestionOutput(match.LeftPlayer.UserId, match.RightPlayer.UserId, question.Id));
    }
}

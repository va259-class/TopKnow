using MediatR;
using TopKnow.Data.Context;
using TopKnow.Entities.Game;
using TopKnow.Modules.Common.Helpers;
using Microsoft.EntityFrameworkCore;

namespace TopKnow.Modules.PlayGround.Commands.Matches;

public class CreateMatchRequest : IRequest<Result<Guid>>
{
    public CreateMatchRequest(Guid requesterId, Guid challengerId)
    {
        this.RequesterId = requesterId;
        this.ChallengerId = challengerId;
    }

    public Guid RequesterId { get; }
    public Guid ChallengerId { get; }
}

internal class CreateMatchCommandHandler : IRequestHandler<CreateMatchRequest, Result<Guid>>
{
    private readonly TopKnowContext context;

    public CreateMatchCommandHandler(TopKnowContext context)
    {
        this.context = context;
    }
    public async Task<Result<Guid>> Handle(CreateMatchRequest request, CancellationToken cancellationToken)
    {
        var requesterPlayerId = await context.Players.Where(f => f.UserId == request.RequesterId)
                                               .Select(s => s.Id)
                                               .FirstOrDefaultAsync(cancellationToken);

        var challengerPlayerId = await context.Players.Where(f => f.UserId == request.ChallengerId)
                                               .Select(s => s.Id)
                                               .FirstOrDefaultAsync(cancellationToken);

        var match = new Match
        {
            Id = Guid.NewGuid(),
            LeftPlayerId = requesterPlayerId,
            RightPlayerId = challengerPlayerId,
            RoundCount = 1
        };

        context.Matches.Add(match);
        await context.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(match.Id);
    }
}

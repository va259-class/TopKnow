using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopKnow.Data.Context;
using TopKnow.Entities.Game;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.PlayGround.Commands.Matches;

public class CreateMatchRequest : IRequest<Result<bool>>
{
    public CreateMatchRequest(Guid requesterId, Guid challengerId)
    {
        this.RequesterId = requesterId;
        this.ChallengerId = challengerId;
    }

    public Guid RequesterId { get; }
    public Guid ChallengerId { get; }
}

internal class CreateMatchCommandHandler : IRequestHandler<CreateMatchRequest, Result<bool>>
{
    private readonly TopKnowContext context;

    public CreateMatchCommandHandler(TopKnowContext context)
    {
        this.context = context;
    }
    public async Task<Result<bool>> Handle(CreateMatchRequest request, CancellationToken cancellationToken)
    {
        return Result<bool>.Success(true);

        var match = new Match
        {
            //Doldurulacak
        };

        context.Matches.Add(match);
        await context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Success(true);
    }
}

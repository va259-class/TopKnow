using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TopKnow.Common.Concretes;
using TopKnow.Common.Enums;
using TopKnow.Data.Context;
using TopKnow.Entities.Game;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.Management.Queries.Users;

//Giriş parametreleri
public class PagedUserRequestInput : PagedQueryParameter
{
    public Guid? UserId { get; set; }
}

//Çıkış değeri
public record PlayersRequestOutput(Guid Id, string NickName, Guid UserId, string DisplayName);

// Mediator Request'i
public class GetPagedPlayersRequest : IRequest<Result<List<PlayersRequestOutput>>>
{
    public GetPagedPlayersRequest(PagedUserRequestInput input)
    {
        if (input.Page == 0)
        {
            input.Page = 1;
        }
        if (input.Size == 0)
        {
            input.Size = 15;
        }
        Input = input;
    }

    public PagedUserRequestInput Input { get; }
}

//İşin yapıldığı yer
internal class GetPagedPlayersQueryHandler : IRequestHandler<GetPagedPlayersRequest, Result<List<PlayersRequestOutput>>>
{
    private readonly TopKnowContext context;

    public GetPagedPlayersQueryHandler(TopKnowContext context)
    {
        this.context = context;
    }

    public async Task<Result<List<PlayersRequestOutput>>> Handle(GetPagedPlayersRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<Player, bool>> predicate = f => !f.IsDeleted && f.User.Type == UserType.User;
        if (request.Input.UserId.HasValue)
        {
            predicate = predicate.And(f => f.UserId == request.Input.UserId.Value);
        }
        var players = await context.Players.Where(predicate)
                                           .Select(p => new PlayersRequestOutput(p.Id, p.NickName, p.UserId, p.User.DisplayName))
                                           .ToListAsync(cancellationToken);

        if (players.Count == 0)
        {
            return Result<List<PlayersRequestOutput>>.Failure(new Error(ErrorCodes.NOT_FOUND, "No Player Found"));
        }

        return Result<List<PlayersRequestOutput>>.Success(players);
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using TopKnow.Common.Concretes;
using TopKnow.Common.Enums;
using TopKnow.Data.Context;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.Management.Queries.Users;

//Çıkış değerleri
public record AdminUserRequestOutput(Guid Id, string DisplayName);

//Mediator requesti
public class GetPagedAdminUsersRequest : IRequest<Result<List<AdminUserRequestOutput>>>
{
    public GetPagedAdminUsersRequest(PagedQueryParameter input)
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

    public PagedQueryParameter Input { get; }
}

//query işletici
internal class GetPagedAdminUsersQueryHandler : IRequestHandler<GetPagedAdminUsersRequest, Result<List<AdminUserRequestOutput>>>
{
    private readonly TopKnowContext context;

    public GetPagedAdminUsersQueryHandler(TopKnowContext context)
    {
        this.context = context;
    }
    public async Task<Result<List<AdminUserRequestOutput>>> Handle(GetPagedAdminUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await context.Users.Where(f => f.Type == UserType.Admin && !f.IsDeleted)
                                       .Select(s => new AdminUserRequestOutput(s.Id, s.DisplayName))
                                       .Skip((request.Input.Page - 1) * request.Input.Size)
                                       .Take(request.Input.Size)
                                       .ToListAsync(cancellationToken);

        if (users.Count == 0)
        {
            return Result<List<AdminUserRequestOutput>>.Failure(new Error(ErrorCodes.NOT_FOUND, "No admin found"));
        }

        return Result<List<AdminUserRequestOutput>>.Success(users);
    }
}

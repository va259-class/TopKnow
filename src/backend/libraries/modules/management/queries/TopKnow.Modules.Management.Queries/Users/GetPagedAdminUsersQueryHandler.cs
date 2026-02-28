using MediatR;
using Microsoft.EntityFrameworkCore;
using TopKnow.Common.Concretes;
using TopKnow.Common.Enums;
using TopKnow.Data.Context;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.Management.Queries.Users;

public record AdminUser(Guid Id, string DisplayName);

public class GetPagedAdminUsersRequest : IRequest<Result<List<AdminUser>>>
{
    public GetPagedAdminUsersRequest(PagedQueryParameter parameter)
    {
        Parameter = parameter;
    }

    public PagedQueryParameter Parameter { get; }
}

internal class GetPagedAdminUsersQueryHandler : IRequestHandler<GetPagedAdminUsersRequest, Result<List<AdminUser>>>
{
    private readonly TopKnowContext context;

    public GetPagedAdminUsersQueryHandler(TopKnowContext context)
    {
        this.context = context;
    }
    public async Task<Result<List<AdminUser>>> Handle(GetPagedAdminUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await context.Users.Where(f => f.Type == UserType.Admin && !f.IsDeleted)
                                       .Select(s => new AdminUser(s.Id, s.DisplayName))
                                       .ToListAsync(cancellationToken);

        if (users.Count == 0)
        {
            return Result<List<AdminUser>>.Failure(new Error(ErrorCodes.NOT_FOUND, "No admin found"));
        }

        return Result<List<AdminUser>>.Success(users);
    }
}

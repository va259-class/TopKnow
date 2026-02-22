using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopKnow.Common.Concretes;
using TopKnow.Common.Enums;
using TopKnow.Data.Context;

namespace TopKnow.Modules.Management.Queries.Users;

public class GetPagedAdminUsersRequest : IRequest
{
    public GetPagedAdminUsersRequest(PagedQueryParameter parameter)
    {
        Parameter = parameter;
    }

    public PagedQueryParameter Parameter { get; }
}

internal class GetPagedAdminUsersQueryHandler : IRequestHandler<GetPagedAdminUsersRequest>
{
    private readonly TopKnowContext context;

    public GetPagedAdminUsersQueryHandler(TopKnowContext context)
    {
        this.context = context;
    }
    public Task Handle(GetPagedAdminUsersRequest request, CancellationToken cancellationToken)
    {
        var users = context.Users.Where(f => f.Type == UserType.Admin && !f.IsDeleted).ToList();
        throw new NotImplementedException();
    }
}

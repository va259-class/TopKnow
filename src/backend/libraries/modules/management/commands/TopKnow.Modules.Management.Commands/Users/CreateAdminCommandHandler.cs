using MediatR;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TopKnow.Common.Enums;
using TopKnow.Data.Context;
using TopKnow.Entities.Main;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.Management.Commands.Users;

public class CreateAdminUserRequestInput
{
    [Required]
    [MaxLength(32)]
    public string DisplayName { get; set; }
    [Required]
    [MaxLength(20)]
    public string EMail { get; set; }
    [Required]
    [MaxLength(16)]
    public string Password { get; set; }
}

public class CreateAdminUserRequest : IRequest<Result<bool>>
{
    public CreateAdminUserRequest(CreateAdminUserRequestInput input)
    {
        Input = input;
    }

    public CreateAdminUserRequestInput Input { get; }
}
internal class CreateAdminCommandHandler : IRequestHandler<CreateAdminUserRequest, Result<bool>>
{
    private readonly TopKnowContext context;

    public CreateAdminCommandHandler(TopKnowContext context)
    {
        this.context = context;
    }
    public async Task<Result<bool>> Handle(CreateAdminUserRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(f => f.Mail == request.Input.EMail, cancellationToken);
        if (existingUser is not null)
        {
            return Result<bool>.Failure(new Error(ErrorCodes.ALREADY_EXISTS, request.Input.EMail));
        }
        var user = new User
        {
            Id = Guid.NewGuid(),
            DisplayName = request.Input.DisplayName,
            Mail = request.Input.EMail,
            Password = request.Input.Password,
            Type = UserType.Admin
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Success(true);
    }
}

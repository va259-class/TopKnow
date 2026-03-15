using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TopKnow.Common.Enums;
using TopKnow.Data.Context;
using TopKnow.Entities.Main;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.PlayGround.Commands.Authentication;

public record RegisterUserInput(string Mail, string DisplayName, string Password);

public class RegisterUserRequest : IRequest<Result<bool>>
{
    public RegisterUserRequest(RegisterUserInput input)
    {
		Input = input;
	}

	public RegisterUserInput Input { get; }
}

internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserRequest, Result<bool>>
{
	private readonly TopKnowContext context;

	public RegisterUserCommandHandler(TopKnowContext context)
    {
		this.context = context;
	}
    public async Task<Result<bool>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
	{
		var existingUser = await context.Users.AsNoTracking()
											  .FirstOrDefaultAsync(f => f.Mail == request.Input.Mail, cancellationToken);

		if (existingUser is not null)
		{
			return Result<bool>.Failure(new Error(ErrorCodes.ALREADY_EXISTS, request.Input.Mail));
		}
		var id = Guid.NewGuid();
		var hasher = new PasswordHasher<object>();

		var entity = new User
		{
			Id = id,
			Mail = request.Input.Mail,
			DisplayName = request.Input.DisplayName,
			Password = hasher.HashPassword(id, request.Input.Password),
			Type = UserType.User,
		};

		context.Users.Add(entity);
		await context.SaveChangesAsync(cancellationToken);

		return Result<bool>.Success(true);
	}
}

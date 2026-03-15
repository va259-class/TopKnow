using MediatR;
using Microsoft.EntityFrameworkCore;
using TopKnow.Data.Context;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.Management.Queries.Questions;

public record QuestionTypeItemOutput(Guid Id, string Name);

public class GetQuestionTypesRequest : IRequest<Result<List<QuestionTypeItemOutput>>>
{
}

internal class GetQuestionTypesQueryHandler : IRequestHandler<GetQuestionTypesRequest, Result<List<QuestionTypeItemOutput>>>
{
	private readonly TopKnowContext context;

	public GetQuestionTypesQueryHandler(TopKnowContext context)
	{
		this.context = context;
	}

	public async Task<Result<List<QuestionTypeItemOutput>>> Handle(GetQuestionTypesRequest request, CancellationToken cancellationToken)
	{
		var lookUpType = await context.LookUpTypes
									  .AsNoTracking() // EF kullanırken okunacak kaydı takip eilmemesini sadece okuma amaçlı bu kaydı çekildiğini söylemek için kullanılır
									  .FirstOrDefaultAsync(lt => lt.Name == "QuestionType", cancellationToken);

		List<QuestionTypeItemOutput> types;
		if (lookUpType is null)
		{
			return Result<List<QuestionTypeItemOutput>>.Failure(new Error(ErrorCodes.NOT_FOUND, "QuestionType not Found"));
		}

		types = await context.LookUps.AsNoTracking()
									 .Where(l => l.TypeId == lookUpType.Id && !l.IsDeleted)
									 .OrderBy(l => l.Order)
									 .Select(l => new QuestionTypeItemOutput(l.Id, l.Name))
									 .ToListAsync(cancellationToken);
		return Result<List<QuestionTypeItemOutput>>.Success(types);
	}
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using TopKnow.Common.Concretes;
using TopKnow.Data.Context;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.Management.Queries.Questions;

public record QuestionListItemOutput(Guid Id, string Title, string TypeName);

public class GetPagedQuestionsRequest : IRequest<Result<List<QuestionListItemOutput>>>
{
    public GetPagedQuestionsRequest(PagedQueryParameter input)
    {
        Input = input;
    }

    public PagedQueryParameter Input { get; }
}

internal class GetPagedQuestionsQueryHandler : IRequestHandler<GetPagedQuestionsRequest, Result<List<QuestionListItemOutput>>>
{
    private readonly TopKnowContext _context;

    public GetPagedQuestionsQueryHandler(TopKnowContext context)
    {
        _context = context;
    }

    public async Task<Result<List<QuestionListItemOutput>>> Handle(GetPagedQuestionsRequest request, CancellationToken cancellationToken)
    {
        if (request.Input.Page <= 0)
        {
            return Result<List<QuestionListItemOutput>>.Failure(new Error(ErrorCodes.INVALID_PARAMETER, "Page pozitif olmalıdır"));
        }
        if (request.Input.Size <= 0 || request.Input.Size > 50)
        {
            return Result<List<QuestionListItemOutput>>.Failure(new Error(ErrorCodes.INVALID_PARAMETER, "Size 1-50 arasında olmalıdır"));
        }

        var items = await _context.Questions.AsNoTracking()
                                            .Where(q => !q.IsDeleted)
                                            .Include(q => q.Type)
                                            .OrderByDescending(q => q.CreatedAt)
                                            .Skip((request.Input.Page - 1) * request.Input.Size)
                                            .Take(request.Input.Size)
                                            .Select(q => new QuestionListItemOutput(q.Id, q.Title, q.Type != null ? q.Type.Name : ""))
                                            .ToListAsync(cancellationToken);

        if (items.Count == 0)
        {
            return Result<List<QuestionListItemOutput>>.Empty();
        }
        return Result<List<QuestionListItemOutput>>.Success(items);
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopKnow.Data.Context;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Modules.PlayGround.Queries.Questions;

public record GetQuestionWithAnswersOutput(Guid Id, string Question, List<string> Answers, int CorrectIndex);

public class GetQuestionWithAnswersRequest : IRequest<Result<GetQuestionWithAnswersOutput>>
{
    public GetQuestionWithAnswersRequest(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

internal class GetQuestionWithAnswersQueryHandler : IRequestHandler<GetQuestionWithAnswersRequest, Result<GetQuestionWithAnswersOutput>>
{
    private readonly TopKnowContext context;

    public GetQuestionWithAnswersQueryHandler(TopKnowContext context)
    {
        this.context = context;
    }

    public async Task<Result<GetQuestionWithAnswersOutput>> Handle(GetQuestionWithAnswersRequest request, CancellationToken cancellationToken)
    {
        var question = await context.Questions.AsNoTracking()
                                              .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        int correctIndex = question.Answers.FindIndex(a => a.IsCorrect);
        var result = new GetQuestionWithAnswersOutput(request.Id, 
                                                      question.Title, 
                                                      question.Answers.Select(s => s.Title).ToList(), 
                                                      correctIndex);

        return Result< GetQuestionWithAnswersOutput>.Success(result);
    }
}

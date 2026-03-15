using MediatR;
using Microsoft.AspNetCore.Mvc;
using TopKnow.Common.Concretes;
using TopKnow.Modules.Management.Commands.Questions;
using TopKnow.Modules.Management.Queries.Questions;

namespace TopKnow.Management.Api.Controllers;

[ApiController]
[Route("api/questions")]
public class QuestionController : TopKnowController
{
    private readonly IMediator _mediator;

    public QuestionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] PagedQueryParameter parameter, CancellationToken cancellationToken)
    {
        var request = new GetPagedQuestionsRequest(parameter);
        var result = await _mediator.Send(request, cancellationToken);
        return AsResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetQuestionByIdRequest(id);
        var result = await _mediator.Send(request, cancellationToken);
        return AsResult(result);
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetTypes(CancellationToken cancellationToken)
    {
        var request = new GetQuestionTypesRequest();
        var result = await _mediator.Send(request, cancellationToken);
        return AsResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuestionRequestInput input, CancellationToken cancellationToken)
    {
        var request = new CreateQuestionRequest(input);
        var result = await _mediator.Send(request, cancellationToken);
        return AsResult(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateQuestionRequestInput input, CancellationToken cancellationToken)
    {
        if (id != input.Id)
            return BadRequest();
        var request = new UpdateQuestionRequest(input);
        var result = await _mediator.Send(request, cancellationToken);
        return AsResult(result);
    }
}

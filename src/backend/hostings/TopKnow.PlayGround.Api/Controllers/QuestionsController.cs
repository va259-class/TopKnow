using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopKnow.Modules.PlayGround.Queries.Questions;

namespace TopKnow.PlayGround.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/questions")]
public class QuestionsController : ControllerBase
{
    private readonly IMediator mediator;

    public QuestionsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetQuestionWithAnswersRequest(id), cancellationToken);
        return Ok(result.Value);
    }
}

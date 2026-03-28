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

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int count = 5)
    {
        var result = await mediator.Send(new GetRandomQuestionsRequest(count));
        
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}

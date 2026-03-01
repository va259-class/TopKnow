using Microsoft.AspNetCore.Mvc;
using TopKnow.Modules.Common.Helpers;

namespace TopKnow.Management.Api;

public abstract class TopKnowController : ControllerBase
{
	[NonAction]
	protected IActionResult ToResult(Result result)
	{
		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else if (!result.IsSuccess && result.Error is null)
		{
			return NoContent();
		}
		return BadRequest(result.Error);
	}

	[NonAction]
	protected IActionResult ToResult<T>(Result<T> result)
	{
		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else if (!result.IsSuccess && result.Error is null)
		{
			return NoContent();
		}
		return BadRequest(result.Error);
	}
}

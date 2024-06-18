namespace server.Controllers;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using Data.Entities;
using Application.Commands.Users;
using Application.Queries.Users;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Add([FromBody] AddUser request)
    {
        var response = await _mediator.Send(request);

        if (response.IsFailed) return BadRequest(response.Errors.ToErrorResponse());
        var user = response.Value;
        return CreatedAtAction(nameof(Add), new { sub = user.Sub }, user);
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateUser request)
    {
        var response = await _mediator.Send(request);

        if (response.IsFailed) return BadRequest(response.Errors.ToErrorResponse());
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetUserBySub([FromQuery] GetUserBySub request)
    {
        var response = await _mediator.Send(request);

        if (response.IsFailed) return BadRequest(response.Errors.ToErrorResponse());
        return Ok(response.Value);
    }
}
namespace server.Controllers;

using Microsoft.AspNetCore.Mvc;
using server.Data.Repositories;
using server.Entities;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly ILogger<UserController> _logger;

    public UserController(UserRepository userRepository, ILogger<UserController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post([FromBody] User user)
    {
        try
        {
            await _userRepository.Add(user);
            return CreatedAtAction(nameof(Post), new { sub = user.Sub }, user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to add user with Sub: {Sub}", user.Sub);
            return StatusCode(500, "Internal server error");
        }
    }
}
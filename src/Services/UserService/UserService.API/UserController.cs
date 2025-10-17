using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UserService.API;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet("hello/{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation($"called id: {id}");
        return Ok(id);
    }
    [HttpGet("break")]
    public IActionResult Break()
    {
        throw new Exception("test");
    }
}
using Microsoft.AspNetCore.Mvc;
using UdvTestTask.Abstractions;
using UdvTestTask.Models;

namespace UdvTestTask.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Auth([FromBody] UserModel user)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var authResult = await _authService.TryAuthAsync(user);
        if (!authResult.Ok)
            return Unauthorized();
        
        return Ok();
    }
}
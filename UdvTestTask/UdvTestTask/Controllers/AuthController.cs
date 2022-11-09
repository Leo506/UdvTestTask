using Microsoft.AspNetCore.Mvc;
using UdvTestTask.Abstractions;
using UdvTestTask.Models;

namespace UdvTestTask.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

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
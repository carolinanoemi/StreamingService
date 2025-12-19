using ApiGateway.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Presentation.Controllers;

[ApiController]
[Route("faucet")]
public class FaucetController : ControllerBase
{
    private readonly JwtTokenService _jwt;

    public FaucetController(JwtTokenService jwt)
    {
        _jwt = jwt;
    }

    // GET /faucet?username=caro
    [HttpGet]
    public IActionResult GetToken([FromQuery] string username = "demo-user")
    {
        var token = _jwt.CreateToken(username);
        return Ok(new { access_token = token });
    }
}

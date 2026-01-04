using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RatingService.Infrastructure;
using RatingService.Infrastructure.Data;

namespace WatchHistoryService.Api.Controllers;

[ApiController]
[Route("api/ping")]
public class PingController : ControllerBase
{
    private readonly RatingDbContext _db;

    public PingController(RatingDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Ping()
    {
        var canConnect = await _db.Database.CanConnectAsync();
        return Ok(new { service = "RatingService", db = canConnect });
    }
}

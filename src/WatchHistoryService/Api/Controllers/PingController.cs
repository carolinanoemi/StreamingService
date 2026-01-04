using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchHistoryService.Infrastructure;
using WatchHistoryService.Infrastructure.Data;

namespace WatchHistoryService.Controllers;

[ApiController]
[Route("api/ping")]
public class PingController : ControllerBase
{
    private readonly WatchHistoryDbContext _db;

    public PingController(WatchHistoryDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Ping()
    {
        var canConnect = await _db.Database.CanConnectAsync();
        return Ok(new { service = "WatchHistoryService", db = canConnect });
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Infrastructure;
using SubscriptionService.Infrastructure.Data;

namespace WatchHistoryService.Api.Controllers;

[ApiController]
[Route("api/ping")]


public class PingController : ControllerBase
{
    private readonly SubscriptionDbContext _db;

    public PingController(SubscriptionDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Ping()
    {
        var canConnect = await _db.Database.CanConnectAsync();
        return Ok(new { service = "SubscriptionService", db = canConnect });
    }
}

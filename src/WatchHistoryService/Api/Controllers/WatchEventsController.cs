using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchHistoryService.Domain.Entities;
using WatchHistoryService.Infrastructure.Data;
using WatchHistoryService.Api.Dtos;

namespace WatchHistoryService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WatchEventsController : ControllerBase
{
    private readonly WatchHistoryDbContext _db;

    public WatchEventsController(WatchHistoryDbContext db)
    {
        _db = db;
    }

    // GET: /api/WatchEvents/recent/1
    [HttpGet("recent/{userId:int}")]
    public async Task<ActionResult<IEnumerable<WatchEvent>>> GetRecentForUser(int userId)
    {
        var items = await _db.WatchEvents
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.WatchedAt)
            .Take(20)
            .ToListAsync();

        return Ok(items);
    }

    // POST: /api/WatchEvents
    [HttpPost]
    public async Task<ActionResult<WatchEvent>> Create([FromBody] WatchEventDto dto)
    {
        var entity = new WatchEvent
        {
            UserId = dto.UserId,
            VideoId = dto.VideoId,
            WatchedAt = dto.WatchedAt,
            SecondsWatched = dto.SecondsWatched,
            PositionSeconds = dto.PositionSeconds,
            DeviceType = dto.DeviceType
        };

        _db.WatchEvents.Add(entity);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = entity.WatchEventId }, entity);
    }

    // GET: /api/WatchEvents/123
    [HttpGet("{id:int}")]
    public async Task<ActionResult<WatchEvent>> GetById(int id)
    {
        var item = await _db.WatchEvents.FirstOrDefaultAsync(x => x.WatchEventId == id);
        if (item == null) return NotFound();
        return Ok(item);
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Api.Dtos;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Infrastructure.Data;

namespace SubscriptionService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly SubscriptionDbContext _db;

    public SubscriptionsController(SubscriptionDbContext db)
    {
        _db = db;
    }

    // GET /api/Subscriptions/user/1/active
    [HttpGet("user/{userId:int}/active")]
    public async Task<ActionResult<SubscriptionResponseDto>> GetActiveForUser(int userId)
    {
        var s = await _db.Subscriptions
            .Where(x => x.UserId == userId && x.IsActive)
            .OrderByDescending(x => x.StartDate)
            .FirstOrDefaultAsync();

        if (s == null) return NotFound();

        return Ok(new SubscriptionResponseDto
        {
            SubscriptionId = s.SubscriptionId,
            UserId = s.UserId,
            PlanId = s.PlanId,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            IsActive = s.IsActive
        });
    }

    // POST /api/Subscriptions
    [HttpPost]
    public async Task<ActionResult<SubscriptionResponseDto>> Create([FromBody] CreateSubscriptionDto dto)
    {
        // super simpel “regel”: luk gammel aktiv subscription for bruger (hvis findes)
        var existing = await _db.Subscriptions
            .Where(x => x.UserId == dto.UserId && x.IsActive)
            .OrderByDescending(x => x.StartDate)
            .FirstOrDefaultAsync();

        if (existing != null)
        {
            existing.IsActive = false;
            existing.EndDate = DateOnly.FromDateTime(DateTime.UtcNow);
        }

        var entity = new Subscription
        {
            UserId = dto.UserId,
            PlanId = dto.PlanId,
            StartDate = dto.StartDate,
            EndDate = null,
            IsActive = true
        };

        _db.Subscriptions.Add(entity);
        await _db.SaveChangesAsync();

        var response = new SubscriptionResponseDto
        {
            SubscriptionId = entity.SubscriptionId,
            UserId = entity.UserId,
            PlanId = entity.PlanId,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsActive = entity.IsActive
        };

        return CreatedAtAction(nameof(GetById), new { id = entity.SubscriptionId }, response);
    }

    // GET /api/Subscriptions/123
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SubscriptionResponseDto>> GetById(int id)
    {
        var s = await _db.Subscriptions.FirstOrDefaultAsync(x => x.SubscriptionId == id);
        if (s == null) return NotFound();

        return Ok(new SubscriptionResponseDto
        {
            SubscriptionId = s.SubscriptionId,
            UserId = s.UserId,
            PlanId = s.PlanId,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            IsActive = s.IsActive
        });
    }
}

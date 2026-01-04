using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Api.Dtos;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Infrastructure.Data;

namespace SubscriptionService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlansController : ControllerBase
{
    private readonly SubscriptionDbContext _db;

    public PlansController(SubscriptionDbContext db)
    {
        _db = db;
    }

    // GET /api/Plans
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanResponseDto>>> GetAll()
    {
        var plans = await _db.Plans
            .OrderBy(p => p.PlanId)
            .Select(p => new PlanResponseDto
            {
                PlanId = p.PlanId,
                Name = p.Name,
                PricePerMonth = p.PricePerMonth,
                IsActive = p.IsActive
            })
            .ToListAsync();

        return Ok(plans);
    }

    // POST /api/Plans
    [HttpPost]
    public async Task<ActionResult<PlanResponseDto>> Create([FromBody] CreatePlanDto dto)
    {
        var entity = new Plan
        {
            Name = dto.Name,
            PricePerMonth = dto.PricePerMonth,
            IsActive = dto.IsActive
        };

        _db.Plans.Add(entity);
        await _db.SaveChangesAsync();

        var response = new PlanResponseDto
        {
            PlanId = entity.PlanId,
            Name = entity.Name,
            PricePerMonth = entity.PricePerMonth,
            IsActive = entity.IsActive
        };

        return CreatedAtAction(nameof(GetById), new { id = entity.PlanId }, response);
    }

    // GET /api/Plans/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlanResponseDto>> GetById(int id)
    {
        var p = await _db.Plans.FirstOrDefaultAsync(x => x.PlanId == id);
        if (p == null) return NotFound();

        return Ok(new PlanResponseDto
        {
            PlanId = p.PlanId,
            Name = p.Name,
            PricePerMonth = p.PricePerMonth,
            IsActive = p.IsActive
        });
    }
}

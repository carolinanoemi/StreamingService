using Microsoft.AspNetCore.Mvc;
using NotificationService.Api.Dtos;
using NotificationService.Application.Interfaces;

namespace NotificationService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationResponseDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();

            var response = items.Select(n => new NotificationResponseDto
            {
                Id = n.Id,
                UserId = n.UserId,
                Message = n.Message,
                IsSent = n.IsSent,
                CreatedAt = n.CreatedAt
            });

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<NotificationResponseDto>> GetById(int id)
        {
            var n = await _service.GetByIdAsync(id);
            if (n == null) return NotFound();

            return Ok(new NotificationResponseDto
            {
                Id = n.Id,
                UserId = n.UserId,
                Message = n.Message,
                IsSent = n.IsSent,
                CreatedAt = n.CreatedAt
            });
        }

        // POST: api/notification/send
        [HttpPost("send")]
        public async Task<ActionResult<NotificationResponseDto>> Send([FromBody] SendNotificationDto dto)
        {
            var n = await _service.SendAsync(dto.UserId, dto.Message);

            return Ok(new NotificationResponseDto
            {
                Id = n.Id,
                UserId = n.UserId,
                Message = n.Message,
                IsSent = n.IsSent,
                CreatedAt = n.CreatedAt
            });
        }
    }
}

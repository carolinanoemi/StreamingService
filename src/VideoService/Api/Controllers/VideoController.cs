using Microsoft.AspNetCore.Mvc;
using VideoService.Api.Dtos;
using VideoService.Application.Interfaces;
using VideoService.Infrastructure.Messaging;
using StreamingService.Contracts.VideoEvents;

namespace VideoService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        // Dependency Injection: Vi modtager vores Service og Publisher gennem constructoren.
        // Det sikrer løs kobling og gør det nemt at teste (mocke).
        private readonly IVideoService _videoService;
        private readonly VideoEventPublisher _publisher;

        public VideoController(IVideoService videoService, VideoEventPublisher publisher)
        {
            _videoService = videoService;
            _publisher = publisher;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoResponseDto>>> GetAll()
        {
            // Henter data fra applikationslaget (via Repository)
            var videos = await _videoService.GetAllVideosAsync();

            // Mapping: Vi mapper fra interne Entities til eksterne DTO'er.
            // Det sikrer, at vi ikke udstiller vores database-struktur direkte.
            var response = videos.Select(v => new VideoResponseDto
            {
                Id = v.Id,
                Title = v.Title,
                Description = v.Description,
                Url = v.Url,
                OwnerUserId = v.OwnerUserId,
                IsPublished = v.IsPublished,
                CreatedAt = v.CreatedAt
            });

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VideoResponseDto>> GetById(int id)
        {
            var v = await _videoService.GetVideoByIdAsync(id);

            // Null check: Hvis videoen ikke findes, returnerer vi korrekt HTTP 404.
            if (v == null)
                return NotFound();

            // Igen mapper vi Entity -> DTO før vi sender svar
            var response = new VideoResponseDto
            {
                Id = v.Id,
                Title = v.Title,
                Description = v.Description,
                Url = v.Url,
                OwnerUserId = v.OwnerUserId,
                IsPublished = v.IsPublished,
                CreatedAt = v.CreatedAt
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<VideoResponseDto>> Create([FromBody] CreateVideoDto dto)
        {
            // Gem i databasen først (vigtigt)
            // Hvis dette fejler, bliver der kastet en exception, og koden stopper her (ingen event sendes).
            var v = await _videoService.CreateVideoAsync(dto.Title, dto.Description, dto.Url, dto.OwnerUserId);

            // FIRE-AND-FORGET (RabbitMQ Integration)
            // Prøv at sende et event om, at videoen er oprettet.
            try
            {
                await _publisher.PublishVideoCreatedAsync(new VideoCreatedEvent
                {
                    VideoId = v.Id,
                    OwnerUserId = v.OwnerUserId,
                    Title = v.Title,
                    CreatedAtUtc = v.CreatedAt
                });

                Console.WriteLine($"[PUBLISH] Publishing VideoCreatedEvent: VideoId={v.Id}, Title={v.Title}");
            }
            catch (Exception ex)
            {
                // RESILIENCE: Hvis RabbitMQ er nede, griber vi fejlen her.
                // Vi logger en advarsel, men crasher IKKE requestet.
                // Det betyder, at brugeren stadig får "Succes" (201 Created), selvom notifikationen måske mangler.
                Console.WriteLine($"[WARN] Failed to publish VideoCreatedEvent: {ex.Message}");
            }

            // Mapper den nyoprettede video til DTO
            var response = new VideoResponseDto
            {
                Id = v.Id,
                Title = v.Title,
                Description = v.Description,
                Url = v.Url,
                OwnerUserId = v.OwnerUserId,
                IsPublished = v.IsPublished,
                CreatedAt = v.CreatedAt
            };

            // Returnerer 201 Created med en lokation header til den nye ressource
            return CreatedAtAction(nameof(GetById), new { id = v.Id }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<VideoResponseDto>> Update(int id, [FromBody] UpdateVideoDto dto)
        {
            // Kalder update i servicen
            var v = await _videoService.UpdateVideoAsync(id, dto.Title, dto.Description);

            // Hvis update returnerer null, betyder det, at ID'et ikke fandtes: 404 error
            if (v == null)
                return NotFound();

            var response = new VideoResponseDto
            {
                Id = v.Id,
                Title = v.Title,
                Description = v.Description,
                Url = v.Url,
                OwnerUserId = v.OwnerUserId,
                IsPublished = v.IsPublished,
                CreatedAt = v.CreatedAt
            };

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Unpublish(int id)
        {
            // Sletter eller "unpubliserer" videoen
            var ok = await _videoService.UnpublishVideoAsync(id);

            if (!ok)
                return NotFound();

            // Ved sletning returnerer man typisk 204 No Content (Succes, men ingen data tilbage)
            return NoContent();
        }
    }
}
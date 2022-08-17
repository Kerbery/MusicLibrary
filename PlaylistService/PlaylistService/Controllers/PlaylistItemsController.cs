using Common;
using Microsoft.AspNetCore.Mvc;
using PlaylistService.DTOs;
using PlaylistService.DTOs.PlaylistItemDTOs;
using PlaylistService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaylistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistItemsController : ControllerBase
    {
        private readonly IRepository<PlaylistItem> playlistItemRepository;
        private readonly IRepository<Track> trackRepository;

        public PlaylistItemsController(IRepository<PlaylistItem> playlistItemsRepository, IRepository<Track> trackRepository)
        {
            this.playlistItemRepository = playlistItemsRepository;
            this.trackRepository = trackRepository;
        }


        // GET: api/<PlaylistItemsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPlaylistItemDTO>>> GetPLaylistItemsAsync([FromQuery] Guid playlistId)
        {
            var playlistItems = (await playlistItemRepository
                    .GetAllAsync(playlistItem => playlistId == Guid.Empty || playlistItem.PlaylistId == playlistId))
                    .Select(async playlistItem =>
                    {
                        var track = await trackRepository.GetAsync(playlistItem.TrackId);
                        return playlistItem.AsDTO(track.AsDTO());
                    });

            return Ok(playlistItems);
        }

        // GET api/<PlaylistItemsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPlaylistItemDTO>> GetAsync(Guid id)
        {
            var playlistItem = await playlistItemRepository.GetAsync(id);
            if (playlistItem == null)
            {
                return NotFound();
            }

            return Ok(playlistItem);
        }

        // POST api/<PlaylistItemsController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] CreatePlaylistItemDTO createPlaylistItemDTO)
        {
            PlaylistItem playlistItem = new()
            {
                Id = Guid.NewGuid(),
                TrackId = createPlaylistItemDTO.TrackId,
                PlaylistId = createPlaylistItemDTO.PlaylistId,
                Position = 0,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            await playlistItemRepository.CreateAsync(playlistItem);
            var track = await trackRepository.GetAsync(playlistItem.TrackId);

            return CreatedAtAction(nameof(GetAsync), new { id = playlistItem.Id }, playlistItem.AsDTO(track.AsDTO()));
        }

        // PUT api/<PlaylistItemsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] UpdatePlaylistItemDTO updatePlaylistItemDTO)
        {
            var existingPlaylistItem = await playlistItemRepository.GetAsync(id);

            if (existingPlaylistItem == null)
            {
                return NotFound();
            }

            existingPlaylistItem.Position = updatePlaylistItemDTO.Position;

            await playlistItemRepository.UpdateAsync(existingPlaylistItem);

            return Ok();
        }

        // DELETE api/<PlaylistItemsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existingPlaylistItem = await playlistItemRepository.GetAsync(id);

            if (existingPlaylistItem == null)
            {
                return NotFound();
            }

            await playlistItemRepository.RemoveAsync(id);

            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaylistService.Data;
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
        private readonly PlaylistContext context;

        public PlaylistItemsController(PlaylistContext context)
        {
            this.context = context;
        }


        // GET: api/<PlaylistItemsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPlaylistItemDTO>>> GetPLaylistItemsAsync([FromQuery] Guid playlistId)
        {
            var playlistItems = await context.PlaylistItems
                .Where(pi=>pi.PlaylistId == playlistId)
                .Include(pi=>pi.Track)
                .Select(pi=>pi.AsDTO())
                .ToListAsync();

            return Ok(playlistItems);
        }

        // GET api/<PlaylistItemsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPlaylistItemDTO>> GetAsync(Guid id)
        {
            var playlistItem = await context.PlaylistItems
                .Include(pi=>pi.Track)
                .SingleOrDefaultAsync(pi=>pi.Id==id);

            if (playlistItem == null)
            {
                return NotFound();
            }

            return Ok(playlistItem.AsDTO());
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

            context.PlaylistItems.Add(playlistItem);
            await context.SaveChangesAsync();

            var savedPlaylistItem = await context.PlaylistItems
                .Include(pi => pi.Track)
                .ThenInclude(x => x.User)
                .SingleOrDefaultAsync(pi=>pi.Id==playlistItem.Id);

            return CreatedAtAction(nameof(GetAsync), new { id = playlistItem.Id }, savedPlaylistItem.AsDTO());
        }

        // PUT api/<PlaylistItemsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] UpdatePlaylistItemDTO updatePlaylistItemDTO)
        {
            var existingPlaylistItem = await context.PlaylistItems
                .Include(pi => pi.Track)
                .SingleOrDefaultAsync(pi => pi.Id == id); ;

            if (existingPlaylistItem == null)
            {
                return NotFound();
            }

            existingPlaylistItem.Position = updatePlaylistItemDTO.Position;

            await context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<PlaylistItemsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existingPlaylistItem = await context.PlaylistItems.FindAsync(id);

            if (existingPlaylistItem == null)
            {
                return NotFound();
            }

            context.PlaylistItems.Remove(existingPlaylistItem);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}

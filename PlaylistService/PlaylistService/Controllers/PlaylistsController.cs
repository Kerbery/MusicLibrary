using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaylistService.Data;
using PlaylistService.DTOs;
using PlaylistService.DTOs.PlaylistDTOs;
using PlaylistService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaylistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PlaylistsController : ControllerBase
    {
        private readonly PlaylistContext context;

        public PlaylistsController(PlaylistContext context)
        {
            this.context = context;
        }

        // GET: api/<PlaylistsController>/?userId={userId}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPlaylistDTO>>> GetUserPLaylistsAsync([FromQuery] Guid userId )
        {
            var playlists = await context.Playlists
                .Include(pl => pl.User)
                .Include(pl => pl.Items)
                .ThenInclude(x=>x.Track)
                .ThenInclude(y=>y.User)
                .Select(pl => pl.AsDTO())
                .ToListAsync();

            

            return Ok(playlists);
        }

        // GET api/<PlaylistsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPlaylistDTO>> GetAsync(Guid id)
        {
            var playlist = await context.Playlists
                .Include(pl => pl.User)
                .Include(pl => pl.Items)
                .ThenInclude(x => x.Track)
                .ThenInclude(y => y.User)
                .Where(x=>x.Id == id)
                .Select(pl => pl.AsDTO())
                .SingleOrDefaultAsync();

            return Ok(playlist);
        }

        // POST api/<PlaylistsController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] CreatePLaylistDTO createPLaylistDTO)
        {
            Playlist playlist = new()
            {
                Id = Guid.NewGuid(),
                Title = createPLaylistDTO.Title,
                Description = createPLaylistDTO.Description,
                CreatedDate = DateTimeOffset.UtcNow,
                UserId = Guid.Parse("e6f8d7d7-76d2-4e8e-8371-ba8d732e94e9"),
                Kind = PlaylistKind.Likes
            };

            context.Playlists.Add(playlist);
            await context.SaveChangesAsync();


            var savedPlaylist = await context.Playlists.Include(pl=>pl.User).SingleOrDefaultAsync(pl=>pl.Id == playlist.Id);

            return CreatedAtAction( nameof(GetAsync), new { id = playlist.Id}, savedPlaylist.AsDTO());


        }

        // PUT api/<PlaylistsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] UpdatePlaylistDTO updatePlaylistDTO)
        {
            var existingPlaylist = await context.Playlists.FindAsync(id);

            if (existingPlaylist == null)
            {
                return NotFound();
            }

            existingPlaylist.Title = updatePlaylistDTO.Title;
            existingPlaylist.Description = updatePlaylistDTO.Description;

            await context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<PlaylistsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existingPlaylist = await context.Playlists.FindAsync(id);

            if (existingPlaylist == null)
            {
                return NotFound();
            }

            context.Playlists.Remove(existingPlaylist);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}

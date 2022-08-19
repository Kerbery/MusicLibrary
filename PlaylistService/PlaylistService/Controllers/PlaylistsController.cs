using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaylistService.DTOs;
using PlaylistService.DTOs.PlaylistDTOs;
using PlaylistService.DTOs.PlaylistItemDTOs;
using PlaylistService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaylistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaylistsController : ControllerBase
    {
        private readonly IRepository<Playlist> playlistRepository;
        private readonly IRepository<PlaylistItem> playlistItemRepository;
        private readonly IRepository<Track> trackRepository;

        public PlaylistsController(IRepository<Playlist> playlistRepository, IRepository<PlaylistItem> playlistItemRepository, IRepository<Track> trackRepository)
        {
            this.playlistRepository = playlistRepository;
            this.playlistItemRepository = playlistItemRepository;
            this.trackRepository = trackRepository;
        }

        // GET: api/<PlaylistsController>/?userId={userId}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPlaylistDTO>>> GetUserPLaylistsAsync([FromQuery] Guid userId )
        {

            var tracks = await trackRepository.GetAllAsync();

            var playlistItems = (await playlistItemRepository.GetAllAsync())
                .Select(playlistItem => {
                    var track = tracks.SingleOrDefault(track => track.Id == playlistItem.TrackId);
                    return playlistItem.AsDTO(track?.AsDTO());
                    });

            var playlists = (await playlistRepository.GetAllAsync(playlist => userId == Guid.Empty || playlist.UserId == userId))
                .Select(playlist => playlist.AsDTO(playlistItems.Where(playlistItem => playlistItem.PlaylistId == playlist.Id)));

            return Ok(playlists);
        }

        // GET api/<PlaylistsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPlaylistDTO>> GetAsync(Guid id)
        {
            var playlist = await playlistRepository.GetAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }

            var tracks = await trackRepository.GetAllAsync();

            var playlistItems = (await playlistItemRepository.GetAllAsync(playlistItem => playlistItem.PlaylistId == id))
                .Select(playlistItem => {
                    var track = tracks.SingleOrDefault(track => track.Id == playlistItem.TrackId);
                    return playlistItem.AsDTO(track?.AsDTO());
                });

            return Ok(playlist.AsDTO(playlistItems));
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
                UserId = Guid.NewGuid(),

            };

            await playlistRepository.CreateAsync(playlist);

            return CreatedAtAction( nameof(GetAsync), new { id = playlist.Id}, playlist.AsDTO(Enumerable.Empty<GetPlaylistItemDTO>()));
        }

        // PUT api/<PlaylistsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] UpdatePlaylistDTO updatePlaylistDTO)
        {
            var existingPlaylist = await playlistRepository.GetAsync(id);

            if (existingPlaylist == null)
            {
                return NotFound();
            }

            existingPlaylist.Title = updatePlaylistDTO.Title;
            existingPlaylist.Description = updatePlaylistDTO.Description;

            await playlistRepository.UpdateAsync(existingPlaylist);

            return Ok();
        }

        // DELETE api/<PlaylistsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existingPlaylist = await playlistRepository.GetAsync(id);

            if (existingPlaylist == null)
            {
                return NotFound();
            }

            await playlistRepository.RemoveAsync(id);

            return Ok();
        }
    }
}

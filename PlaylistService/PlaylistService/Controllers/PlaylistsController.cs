using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaylistService.Data;
using PlaylistService.DTOs;
using PlaylistService.DTOs.PlaylistDTOs;
using PlaylistService.DTOs.PlaylistItemDTOs;
using PlaylistService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaylistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PlaylistsController : ControllerBase
    {
        //private readonly IRepository<Playlist> playlistRepository;
        //private readonly IRepository<PlaylistItem> playlistItemRepository;
        //private readonly IRepository<Track> trackRepository;
        private readonly PlaylistContext context;

        public PlaylistsController(/*IRepository<Playlist> playlistRepository, IRepository<PlaylistItem> playlistItemRepository, IRepository<Track> trackRepository,*/ PlaylistContext context)
        {
            //this.playlistRepository = playlistRepository;
            //this.playlistItemRepository = playlistItemRepository;
            //this.trackRepository = trackRepository;
            this.context = context;
        }

        // GET: api/<PlaylistsController>/?userId={userId}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPlaylistDTO>>> GetUserPLaylistsAsync([FromQuery] Guid userId )
        {

            //var tracks = await trackRepository.GetAllAsync();

            //var playlistItems = (await playlistItemRepository.GetAllAsync())
            //    .Select(playlistItem => {
            //        var track = tracks.SingleOrDefault(track => track.Id == playlistItem.TrackId);
            //        return playlistItem.AsDTO(track?.AsDTO());
            //        });

            //var playlists = (await playlistRepository.GetAllAsync(playlist => userId == Guid.Empty || playlist.UserId == userId))
            //    .Select(playlist => playlist.AsDTO(playlistItems.Where(playlistItem => playlistItem.PlaylistId == playlist.Id)));

            var playlists = await context.Playlists
                .Include(pl => pl.User)
                .Include(pl => pl.Items)
                .ThenInclude(x=>x.Track)
                .Select(pl => pl.AsDTO())
                .ToListAsync();

            

            return Ok(playlists);
        }

        // GET api/<PlaylistsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPlaylistDTO>> GetAsync(Guid id)
        {
            //var playlist = await playlistRepository.GetAsync(id);
            //if (playlist == null)
            //{
            //    return NotFound();
            //}

            //var tracks = await trackRepository.GetAllAsync();

            //var playlistItems = (await playlistItemRepository.GetAllAsync(playlistItem => playlistItem.PlaylistId == id))
            //    .Select(playlistItem => {
            //        var track = tracks.SingleOrDefault(track => track.Id == playlistItem.TrackId);
            //        return playlistItem.AsDTO(track?.AsDTO());
            //    });

            //return Ok(playlist.AsDTO(playlistItems));


            //var userPlaylists = await context.Playlists.Where(pl => pl.UserId == id).ToListAsync();
            var playlist = await context.Playlists.SingleOrDefaultAsync(pl => pl.Id == id);
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
                UserId = Guid.Parse("F32821A4-8810-41F1-9846-542690624EFF"),
                Kind = PlaylistKind.Created
            };

            //await playlistRepository.CreateAsync(playlist);

            context.Playlists.Add(playlist);
            await context.SaveChangesAsync();

            return CreatedAtAction( nameof(GetAsync), new { id = playlist.Id}, playlist.AsDTO(/*Enumerable.Empty<GetPlaylistItemDTO>()*/));


        }

        // PUT api/<PlaylistsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] UpdatePlaylistDTO updatePlaylistDTO)
        {
            //var existingPlaylist = await playlistRepository.GetAsync(id);
            var existingPlaylist = await context.Playlists.FindAsync(id);

            if (existingPlaylist == null)
            {
                return NotFound();
            }

            existingPlaylist.Title = updatePlaylistDTO.Title;
            existingPlaylist.Description = updatePlaylistDTO.Description;

            //await playlistRepository.UpdateAsync(existingPlaylist);
            await context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<PlaylistsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            //var existingPlaylist = await playlistRepository.GetAsync(id);
            var existingPlaylist = await context.Playlists.FindAsync(id);

            if (existingPlaylist == null)
            {
                return NotFound();
            }

            context.Playlists.Remove(existingPlaylist);
            await context.SaveChangesAsync();
            //await playlistRepository.RemoveAsync(id);

            return Ok();
        }
    }
}

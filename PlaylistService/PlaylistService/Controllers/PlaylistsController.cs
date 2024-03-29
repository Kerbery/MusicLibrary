﻿using Microsoft.AspNetCore.Mvc;
using PlaylistService.DTOs.PlaylistDTOs;
using PlaylistService.Business;
using PlaylistService.DTOs.Paging;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaylistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistLogic _playlistService;

        public PlaylistsController(IPlaylistLogic playlistService)
        {
            _playlistService = playlistService;
        }

        // GET: api/<PlaylistsController>/?userId={userId}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPlaylistDTO>>> GetUserPLaylistsAsync([FromQuery] Guid userId, [FromQuery] string userPermalink, [FromQuery] PagingParameters pagingParameters)
        {
            var playlists = await _playlistService.GetUserPlaylists(userId, userPermalink, pagingParameters);

            var metadata = new
            {
                playlists.TotalCount,
                playlists.PageSize,
                playlists.CurrentPage,
                playlists.TotalPages,
                playlists.HasNext,
                playlists.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
            
            return Ok(playlists);
        }

        // GET api/<PlaylistsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPlaylistDTO>> GetAsync(Guid id)
        {
            var playlist = await _playlistService.GetPlaylist(id);

            return Ok(playlist);
        }

        // POST api/<PlaylistsController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] CreatePlaylistDTO createPLaylistDTO)
        {
            var savedPlaylist = await _playlistService.CreatePlaylist(createPLaylistDTO);

            return CreatedAtAction( nameof(GetAsync), new { id = savedPlaylist.PlaylistId}, savedPlaylist);
        }

        // PUT api/<PlaylistsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] UpdatePlaylistDTO updatePlaylistDTO)
        {
            await _playlistService.UpdatePlaylist(id, updatePlaylistDTO);

            return Ok();
        }

        // DELETE api/<PlaylistsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _playlistService.DeletePlaylist(id);

            return Ok();
        }
    }
}

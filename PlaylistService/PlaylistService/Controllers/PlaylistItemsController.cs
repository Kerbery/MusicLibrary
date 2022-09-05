using Microsoft.AspNetCore.Mvc;
using PlaylistService.Business;
using PlaylistService.DTOs.Paging;
using PlaylistService.DTOs.PlaylistItemDTOs;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaylistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistItemsController : ControllerBase
    {
        private readonly IPlaylistItemsLogic _playlistItemsLogic;

        public PlaylistItemsController(IPlaylistItemsLogic playlistItemsLogic)
        {
            _playlistItemsLogic = playlistItemsLogic;
        }


        // GET: api/<PlaylistItemsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPlaylistItemDTO>>> GetPlaylistItemsAsync([FromQuery] Guid playlistId, [FromQuery] PagingParameters pagingParameters)
        {
            var playlistItems = await _playlistItemsLogic.GetPlaylistItems(playlistId, pagingParameters);

            var metadata = new
            {
                playlistItems.TotalCount,
                playlistItems.PageSize,
                playlistItems.CurrentPage,
                playlistItems.TotalPages,
                playlistItems.HasNext,
                playlistItems.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(playlistItems);
        }

        // GET api/<PlaylistItemsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPlaylistItemDTO>> GetAsync(Guid id)
        {
            var playlistItem = await _playlistItemsLogic.GetPlaylistItem(id);

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
            var savedPlaylistItem = await _playlistItemsLogic.CreatePlaylistItem(createPlaylistItemDTO);

            return CreatedAtAction(nameof(GetAsync), new { id = savedPlaylistItem.Id }, savedPlaylistItem);
        }

        // PUT api/<PlaylistItemsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] UpdatePlaylistItemDTO updatePlaylistItemDTO)
        {
            await _playlistItemsLogic.UpdatePlaylistItem(id, updatePlaylistItemDTO);

            return Ok();
        }

        // DELETE api/<PlaylistItemsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _playlistItemsLogic.DeletePlaylistItem(id);

            return Ok();
        }
    }
}

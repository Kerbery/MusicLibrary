using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaylistService.Business;
using PlaylistService.Data;
using PlaylistService.DTOs.LikedTrackDTOs;
using PlaylistService.DTOs.Paging;
using System.Security.Claims;
using System.Text.Json;

namespace PlaylistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikedTracksController : ControllerBase
    {
        private readonly ILikedTrackLogic _likedTrackLogic;
        private string? CurrentUserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public LikedTracksController(PlaylistContext context, ILikedTrackLogic likedTrackLogic)
        {
            _likedTrackLogic = likedTrackLogic;
        }

        // GET: api/LikedTracks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetLikedTrackDTO>>> GetLikedTracks([FromQuery] Guid userId, [FromQuery] string userPermalink, [FromQuery] PagingParameters parameters)
        {
            var likedTracks = await _likedTrackLogic.GetLikedTracks(userId, userPermalink, parameters);

            var metadata = new
            {
                likedTracks.TotalCount,
                likedTracks.PageSize,
                likedTracks.CurrentPage,
                likedTracks.TotalPages,
                likedTracks.HasNext,
                likedTracks.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(likedTracks);
        }

        // GET: api/LikedTracks/Ids
        [HttpGet("Ids")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Guid>>> GetLikedTracksIds([FromQuery] PagingParameters parameters)
        {
            var likedTracks = await _likedTrackLogic.GetLikedTracksIds(Guid.Parse(CurrentUserId), parameters);

            var metadata = new
            {
                likedTracks.TotalCount,
                likedTracks.PageSize,
                likedTracks.CurrentPage,
                likedTracks.TotalPages,
                likedTracks.HasNext,
                likedTracks.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(likedTracks);
        }

        // PUT: api/LikedTracks
        [HttpPut("{trackId}")]
        [Authorize]
        public async Task<ActionResult<GetLikedTrackDTO>> Like(Guid trackId)
        {
            await _likedTrackLogic.LikeTrack(Guid.Parse(CurrentUserId), trackId);

            return Ok();
        }

        // DELETE: api/LikedTracks
        [HttpDelete("{trackId}")]
        [Authorize]
        public async Task<ActionResult<GetLikedTrackDTO>> Unlike(Guid trackId)
        {
            await _likedTrackLogic.UnLikeTrack(Guid.Parse(CurrentUserId), trackId);

            return Ok();
        }
    }
}

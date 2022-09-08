using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaylistService.Business;
using PlaylistService.Data;
using PlaylistService.DTOs.LikedTrackDTOs;
using PlaylistService.DTOs.Paging;
using System.Text.Json;

namespace PlaylistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikedTracksController : ControllerBase
    {
        private readonly ILikedTrackLogic _likedTrackLogic;

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

        // POST: api/LikedTracks
        [HttpPost(nameof(Like))]
        //[Authorize]
        public async Task<ActionResult<GetLikedTrackDTO>> Like([FromBody] LikeTrackDTO likeTrackDTO)
        {
            var userId = Guid.Parse("5a72d732-7b2e-41c0-95b3-2c3cb98ef37f");

            await _likedTrackLogic.LikeTrack(likeTrackDTO.TrackId, userId);

            return Ok();
        }

        // POST: api/LikedTracks
        [HttpPost(nameof(Unlike))]
        //[Authorize]
        public async Task<ActionResult<GetLikedTrackDTO>> Unlike([FromBody] UnLikeTrackDTO unLikeTrackDTO)
        {
            var userId = Guid.Parse("5a72d732-7b2e-41c0-95b3-2c3cb98ef37f");

            await _likedTrackLogic.UnLikeTrack(unLikeTrackDTO.TrackId, userId);

            return Ok();
        }
    }
}

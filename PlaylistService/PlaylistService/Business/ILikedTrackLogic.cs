using PlaylistService.Data;
using PlaylistService.DTOs;
using PlaylistService.DTOs.LikedTrackDTOs;
using PlaylistService.DTOs.Paging;

namespace PlaylistService.Business
{
    public interface ILikedTrackLogic
    {
        Task<PagedList<GetLikedTrackDTO>> GetLikedTracks(Guid userId, string userPermalink, PagingParameters pagingParameters);
        Task<int> LikeTrack(Guid trackId, Guid userId);
        Task<int> UnLikeTrack(Guid trackId, Guid userId);
    }
}

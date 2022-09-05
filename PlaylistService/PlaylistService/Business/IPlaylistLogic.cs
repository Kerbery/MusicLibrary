using PlaylistService.DTOs;
using PlaylistService.DTOs.Paging;
using PlaylistService.DTOs.PlaylistDTOs;

namespace PlaylistService.Business
{
    public interface IPlaylistLogic
    {
        Task<PagedList<GetPlaylistDTO>> GetUserPlaylists(Guid userId, PagingParameters pagingParameters);
        Task<GetPlaylistDTO> GetPlaylist(Guid playlistId);
        Task<GetPlaylistDTO> CreatePlaylist(CreatePlaylistDTO createPlaylistDTO);
        Task<int> UpdatePlaylist(Guid playlistId, UpdatePlaylistDTO updatePlaylistDTO);
        Task<int> DeletePlaylist(Guid playlistId);
    }
}

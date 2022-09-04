using PlaylistService.DTOs.PlaylistDTOs;

namespace PlaylistService.Business
{
    public interface IPlaylistLogic
    {
        Task<IEnumerable<GetPlaylistDTO>> GetUserPlaylists(Guid userId);
        Task<GetPlaylistDTO> GetPlaylist(Guid playlistId);
        Task<GetPlaylistDTO> CreatePlaylist(CreatePlaylistDTO createPlaylistDTO);
        Task<int> UpdatePlaylist(Guid playlistId, UpdatePlaylistDTO updatePlaylistDTO);
        Task<int> DeletePlaylist(Guid playlistId);
    }
}

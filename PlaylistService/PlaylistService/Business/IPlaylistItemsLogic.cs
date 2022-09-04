using PlaylistService.DTOs.PlaylistItemDTOs;

namespace PlaylistService.Business
{
    public interface IPlaylistItemsLogic
    {
        Task<IEnumerable<GetPlaylistItemDTO>> GetPlaylistItems(Guid playlistId);
        Task<GetPlaylistItemDTO> GetPlaylistItem(Guid id);
        Task<GetPlaylistItemDTO> CreatePlaylistItem(CreatePlaylistItemDTO createPlaylistItemDTO);
        Task<int> UpdatePlaylistItem(Guid id, UpdatePlaylistItemDTO updatePlaylistItemDTO);
        Task<int> DeletePlaylistItem(Guid id);
    }
}

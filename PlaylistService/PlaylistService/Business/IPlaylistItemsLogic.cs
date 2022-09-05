using PlaylistService.DTOs;
using PlaylistService.DTOs.Paging;
using PlaylistService.DTOs.PlaylistItemDTOs;

namespace PlaylistService.Business
{
    public interface IPlaylistItemsLogic
    {
        Task<PagedList<GetPlaylistItemDTO>> GetPlaylistItems(Guid playlistId, PagingParameters pagingParameters);
        Task<GetPlaylistItemDTO> GetPlaylistItem(Guid id);
        Task<GetPlaylistItemDTO> CreatePlaylistItem(CreatePlaylistItemDTO createPlaylistItemDTO);
        Task<int> UpdatePlaylistItem(Guid id, UpdatePlaylistItemDTO updatePlaylistItemDTO);
        Task<int> DeletePlaylistItem(Guid id);
    }
}

using PlaylistService.DTOs.PlaylistDTOs;
using PlaylistService.DTOs.PlaylistItemDTOs;
using PlaylistService.Models;

namespace PlaylistService.DTOs
{
    public static class Extensions
    {
        public static GetPlaylistDTO AsDTO(this Playlist playlist, IEnumerable<GetPlaylistItemDTO> playlistItemDTOs)
        {
            return new GetPlaylistDTO(
                PlaylistId: playlist.Id,
                Title: playlist.Title,
                Description: playlist.Description,
                CreatedDate: playlist.CreatedDate,
                Items: playlistItemDTOs
                );
        }

        public static GetPlaylistItemDTO AsDTO(this PlaylistItem playlistItem)
        {
            return new GetPlaylistItemDTO(
                Id: playlistItem.Id,
                PlaylistId: playlistItem.PlaylistId,
                TrackId: playlistItem.TrackId,
                Position: playlistItem.Position,
                CreatedDate: playlistItem.CreatedDate
                );
        }
    }
}

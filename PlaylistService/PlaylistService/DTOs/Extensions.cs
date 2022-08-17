using PlaylistService.DTOs.PlaylistDTOs;
using PlaylistService.DTOs.PlaylistItemDTOs;
using PlaylistService.DTOs.TrackDTOs;
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

        public static GetPlaylistItemDTO AsDTO(this PlaylistItem playlistItem, GetTrackDTO getTrackDTO)
        {
            return new GetPlaylistItemDTO(
                Id: playlistItem.Id,
                PlaylistId: playlistItem.PlaylistId,
                TrackId: playlistItem.TrackId,
                Position: playlistItem.Position,
                CreatedDate: playlistItem.CreatedDate,
                GetTrackDTO: getTrackDTO
                );
        }
        public static GetTrackDTO AsDTO(this Track track)
        {
            return new GetTrackDTO
                (
                    Id: track.Id,
                    ArtworkUrl: track.ArtworkUrl,
                    UrlId: track.UrlId,
                    Title: track.Title,
                    Description: track.Description,
                    Duration: track.DurationTicks,
                    UploadDate: track.UploadDate
                );
        }
    }
}

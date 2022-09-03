using PlaylistService.DTOs.PlaylistDTOs;
using PlaylistService.DTOs.PlaylistItemDTOs;
using PlaylistService.DTOs.TrackDTOs;
using PlaylistService.Models;

namespace PlaylistService.DTOs
{
    public static class Extensions
    {
        public static GetPlaylistDTO AsDTO(this Playlist playlist)
        {
            return new GetPlaylistDTO(
                PlaylistId: playlist.Id,
                Kind: playlist.Kind.ToString(),
                Title: playlist.Title,
                Description: playlist.Description,
                CreatedDate: playlist.CreatedDate,
                User: playlist.User.AsDTO(),
                Items: playlist.Items.Select(pi=>pi.AsDTO())
                );
        }

        public static GetPlaylistItemDTO AsDTO(this PlaylistItem playlistItem)
        {
            return new GetPlaylistItemDTO(
                Id: playlistItem.Id,
                PlaylistId: playlistItem.PlaylistId,
                TrackId: playlistItem.TrackId,
                Position: playlistItem.Position,
                CreatedDate: playlistItem.CreatedDate,
                GetTrackDTO: playlistItem.Track.AsDTO()
                );
        }
        public static GetTrackDTO AsDTO(this Track track)
        {
            return new GetTrackDTO
                (
                    Id: track.Id,
                    ArtworkUrl: track.ArtworkUrl,
                    Permalink: track.Permalink,
                    Title: track.Title,
                    Description: track.Description,
                    Duration: track.DurationSeconds,
                    UploadDate: track.UploadDate,
                    User: track.User.AsDTO()
                );
        }
        public static GetUserDTO AsDTO(this User user)
        {
            return new GetUserDTO
                (
                    UserId: user.Id,
                    UserName: user.UserName,
                    LastName: user.LastName,
                    FirstName: user.FirstName,
                    Permalink: user.Permalink,
                    AvatarUrl: user.AvatarUrl
                );
        }
    }
}

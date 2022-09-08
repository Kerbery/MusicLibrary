using PlaylistService.DTOs.TrackDTOs;

namespace PlaylistService.DTOs.LikedTrackDTOs
{
    public record GetLikedTrackDTO( DateTimeOffset CreatedDate, GetTrackDTO Track);
    public record LikeTrackDTO(Guid TrackId);
    public record UnLikeTrackDTO(Guid TrackId);
}

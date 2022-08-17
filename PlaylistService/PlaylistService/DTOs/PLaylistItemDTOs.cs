using PlaylistService.DTOs.TrackDTOs;

namespace PlaylistService.DTOs.PlaylistItemDTOs
{
    public record GetPlaylistItemDTO(
        Guid Id,
        Guid PlaylistId,
        Guid TrackId,
        uint Position,
        DateTimeOffset CreatedDate,
        GetTrackDTO GetTrackDTO);
    public record CreatePlaylistItemDTO(Guid PlaylistId, Guid TrackId);
    public record UpdatePlaylistItemDTO(Guid PLaylistItemId, uint Position);
    public record DeletePlaylistItemDTO(Guid PlaylistItemId);
}

namespace PlaylistService.DTOs.PlaylistItemDTOs
{
    public record GetPlaylistItemDTO(
        Guid Id,
        Guid PlaylistId,
        Guid TrackId,
        int Position,
        DateTimeOffset CreatedDate);
    public record CreatePlaylistItemDTO(Guid PlaylistId, Guid TrackId);
    public record UpdatePlaylistItemDTO(Guid PLaylistItemId, int Position);
    public record DeletePlaylistItemDTO(Guid PlaylistItemId);
}

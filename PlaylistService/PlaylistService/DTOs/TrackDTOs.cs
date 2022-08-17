namespace PlaylistService.DTOs.TrackDTOs
{
    public record GetTrackDTO(Guid Id, string Title, string Description, long Duration, string ArtworkUrl, string UrlId, DateTimeOffset UploadDate);
}

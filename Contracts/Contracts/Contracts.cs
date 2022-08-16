namespace Contracts
{
    public record TrackCreated(Guid Id, string Title, string UrlId, TimeSpan Duration, string ArtworkUrl, string Description, DateTimeOffset UploadDate);
    public record TrackUpdated(Guid Id, string Title, string UrlId, TimeSpan Duration, string ArtworkUrl, string Description);
    public record TrackDeleted(Guid Id);
}

namespace Contracts
{
    public record TrackCreated(Guid Id, string Title, string Permalink, TimeSpan Duration, string ArtworkUrl, string Description, DateTimeOffset UploadDate, Guid UserId);
    public record TrackUpdated(Guid Id, string Title, string Permalink, TimeSpan Duration, string ArtworkUrl, string Description, Guid UserId);
    public record TrackDeleted(Guid Id);

    public record UserCreated(Guid Id, string UserName, string Permalink, string LastName, string FirstName, string AvatarUrl);
    public record UserUpdated(Guid Id, string UserName, string Permalink, string LastName, string FirstName, string AvatarUrl);
    public record UserDeleted(Guid Id);
}

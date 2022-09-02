namespace PlaylistService.DTOs
{
    public record GetUserDTO(Guid UserId, string UserName, string Permalink, string LastName, string FirstName, string AvatarUrl);
}

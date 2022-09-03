namespace UserProvider.DTOs
{
    public record GetUserDTO(
        Guid Id,
        string UserName,
        string Permalink, 
        string LastName, 
        string FirstName, 
        string AvatarUrl);

    public record CreateUserDTO(
        Guid Id,
        string UserName,
        string Permalink,
        string LastName,
        string FirstName,
        string AvatarUrl);
    public record UpdateUserDTO(
        Guid Id,
        string UserName,
        string Permalink,
        string LastName,
        string FirstName,
        string AvatarUrl);

    public record RemoveUserDTO(
        Guid Id);
}

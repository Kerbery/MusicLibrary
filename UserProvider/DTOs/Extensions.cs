using UserProvider.Models;

namespace UserProvider.DTOs
{
    public static class Extensions
    {
        public static GetUserDTO AsDTO(this User user)
        {
            return new GetUserDTO
            (
                UserName: user.UserName,
                FirstName: user.FirstName,
                LastName: user.LastName,
                Permalink: user.Permalink,
                AvatarUrl: user.AvatarUrl,
                Id: user.Id
            );
        }
    }
}

using Common;

namespace UserProvider.Models
{

    public class User: IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Permalink { get; set; }

        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
    }
}

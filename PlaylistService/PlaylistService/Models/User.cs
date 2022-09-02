using System.ComponentModel.DataAnnotations;

namespace PlaylistService.Models
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Permalink { get; set; }

        [Required]
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
    }
}

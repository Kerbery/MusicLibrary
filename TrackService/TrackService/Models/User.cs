using Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace TrackService.Models
{
    public class User: IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Permalink { get; set; }

        [Required]
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
    }
}

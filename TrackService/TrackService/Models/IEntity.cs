using System;

namespace TrackService.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
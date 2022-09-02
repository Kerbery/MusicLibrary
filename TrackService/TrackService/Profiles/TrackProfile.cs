using AutoMapper;
using Contracts;
using System;
using TrackService.DTOs.TrackDTOs;
using TrackService.Models;

namespace TrackService.Profiles
{
    public class TrackProfile : Profile
    {
        public TrackProfile()
        {
            CreateMap<TimeSpan, double>().ConvertUsing(new DurationConverter());
            CreateMap<Track, GetTrackDTO>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.TotalSeconds));
            CreateMap<Track, TrackUpdated>();
            CreateMap<Track, TrackCreated>();

        }
    }

    public class DurationConverter : ITypeConverter<TimeSpan, double>
    {
        public double Convert(TimeSpan source, double destination, ResolutionContext context)
        {
            return source.TotalSeconds;
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="PollutionEventProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using System;
    using AutoMapper;
    using ImportLibrary.Resolvers;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PollutionEventProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.PollutionHeader, Tubs.PollutionEvent>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))

                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                // Custom ignores
                .ForMember(destination => destination.FormId, opt => opt.Ignore())
                .ForMember(d => d.PhotoFrames, o => o.Ignore())
                .ForMember(d => d.EezId, o => o.Ignore()) // Handled in AfterMap
                .ForMember(d => d.Location, o => o.Ignore()) // Handled in AfterMap
                // Property mapping
                .ForMember(d => d.IncidentDate, o => o.MapFrom(s => s.GetDate()))
                .ForMember(d => d.IncidentDateOnly, o => o.MapFrom(s => s.DateOnly))
                .ForMember(d => d.IncidentTimeOnly, o => o.MapFrom(s => s.TimeOnly))
                .ForMember(d => d.SeaCode, o => o.ResolveUsing<SeaCodeResolver>().FromMember(s => s.SeaCondition))
                .ForMember(d => d.ActivityType, o => o.ResolveUsing<ActivityTypeResolver>().FromMember(s => s.ActivityType))
                .ForMember(d => d.VesselType, o => o.ResolveUsing<SightedVesselResolver>().FromMember(s => s.VesselType))
                .ForMember(d => d.MarpolStickers, o => o.MapFrom(s => s.Stickers))
                .ForMember(d => d.PhotosTaken, o => o.MapFrom(s => s.HasPhotos))
                .ForMember(d => d.CaptainAware, o => o.MapFrom(s => s.Marpol))
                .ForMember(d => d.CaptainAdvised, o => o.MapFrom(s => s.Infringement))
                .AfterMap((s, d) =>
                {
                    d.EezId = null;
                    d.Location = null;
                    var location = s.EezId.NullSafeTrim();
                    if (!String.IsNullOrEmpty(location))
                    {
                        if (2 == location.Length)
                        {
                            d.EezId = location;
                        }
                        else
                        {
                            d.Location = location;
                        }
                    }
                })
                ;
        }
    }
}

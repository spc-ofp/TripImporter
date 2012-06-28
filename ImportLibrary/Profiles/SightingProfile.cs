// -----------------------------------------------------------------------
// <copyright file="SightingProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using System;
    using AutoMapper;
    using ImportLibrary.ExtensionMethods;
    using ImportLibrary.Resolvers;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL.Entities;

    /// <summary>
    /// AutoMapper profile for GEN-1 sightings
    /// </summary>
    public class SightingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.Sighting, Tubs.Sighting>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard Ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.FormId, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Vessel, o => o.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                // Handled in AfterMap
                .ForMember(d => d.EventDateOnly, o => o.Ignore())
                .ForMember(d => d.EventTimeOnly, o => o.Ignore())
                .ForMember(destination => destination.Ircs,
                           options => options.MapFrom(source => source.CallSign))
                .ForMember(destination => destination.VesselName,
                           options => options.MapFrom(source => source.Name))
                .ForMember(destination => destination.VesselFlag,
                           options => options.MapFrom(source => source.FlagCode))
                .ForMember(destination => destination.EezCode,
                           options => options.MapFrom(source => source.EezId))
                .ForMember(destination => destination.EventDate,
                           options => options.MapFrom(source => source.GetDate()))
                .ForMember(d => d.ActionType, o => o.ResolveUsing<ActionTypeResolver>().FromMember(s => s.ActivityType))
                .ForMember(d => d.VesselType, o => o.ResolveUsing<SightedVesselResolver>().FromMember(s => s.VesselType))
                .AfterMap((s, d) =>
                {
                    d.EventDateOnly = d.EventDate.AtMidnight();
                    d.EventTimeOnly = d.EventDate.TimeOnly();
                })
                ;
        }
    }
}

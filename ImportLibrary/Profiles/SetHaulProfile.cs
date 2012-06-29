// -----------------------------------------------------------------------
// <copyright file="SetHaulProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using System;
    using AutoMapper;
    using ImportLibrary.ExtensionMethods;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SetHaulProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.LonglineSetHaulEvent, Tubs.LongLineSetHaulEvent>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.FishingSet, o => o.Ignore())
                // Custom ignores
                .ForMember(d => d.LogDateOnly, o => o.Ignore()) // Handled in AfterMap
                .ForMember(d => d.LogTimeOnly, o => o.Ignore()) // Handled in AfterMap
                .ForMember(d => d.ActivityType, o => o.Ignore()) // Handled in parent
                // Custom mappings
                .ForMember(d => d.LogDate, o => o.MapFrom(s => s.GetDate()))
                .ForMember(d => d.CloudCover, o => o.MapFrom(s => s.CloudCoverage))
                .ForMember(d => d.EezCode, o => o.MapFrom(s => s.EezId))
                .ForMember(d => d.SeaCode, o => o.MapFrom(s => s.SeaCondition))
                .ForMember(d => d.Sethaul, o => o.MapFrom(s => s.EventType))
                .AfterMap((s, d) =>
                {
                    d.LogDateOnly = d.LogDate.AtMidnight();
                    d.LogTimeOnly = d.LogDate.TimeOnly();
                })
                ;
        }
    }
}

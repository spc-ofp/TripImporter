// -----------------------------------------------------------------------
// <copyright file="SeaDayProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using System;
    using AutoMapper;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SeaDayProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.PsDay, Tubs.PurseSeineSeaDay>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard Ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.FormId, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                .ForMember(d => d.RowVersion, o => o.Ignore())
                // Custom ignores
                .ForMember(destination => destination.Gen3Events, opt => opt.Ignore())
                .ForMember(destination => destination.DiaryPage, opt => opt.Ignore())
                .ForMember(d => d.StartOfDay, o => o.MapFrom(s => s.GetDate()))
                .ForMember(d => d.StartDateOnly, o => o.MapFrom(s => s.DateOnly))
                .ForMember(d => d.StartTimeOnly, o => o.MapFrom(s => s.TimeOnly))
                .ForMember(d => d.UtcStartOfDay, o => o.MapFrom(s => s.GetUtcDate()))
                .ForMember(d => d.UtcDateOnly, o => o.MapFrom(s => s.UtcDateOnly))
                .ForMember(d => d.UtcTimeOnly, o => o.MapFrom(s => s.UtcTimeOnly))
                .ForMember(d => d.FloatingObjectsNoSchool, o => o.MapFrom(s => s.LogNoFish))
                .ForMember(d => d.FloatingObjectsWithSchool, o => o.MapFrom(s => s.LogFish))
                .ForMember(d => d.FadsNoSchool, o => o.MapFrom(s => s.FadNoFish))
                .ForMember(d => d.FadsWithSchool, o => o.MapFrom(s => s.FadFish))
                .ForMember(d => d.Activities, o => o.MapFrom(s => s.LogEntries))
                .AfterMap((s,d) =>
                {
                    foreach (var activity in d.Activities)
                    {
                        activity.Day = d;
                    }
                })
                ;
        }
    }
}

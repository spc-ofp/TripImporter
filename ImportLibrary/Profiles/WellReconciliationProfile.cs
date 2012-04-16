// -----------------------------------------------------------------------
// <copyright file="WellReconciliationProfile.cs" company="Secretariat of the Pacific Community">
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
    public class WellReconciliationProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.PsWellReconciliation, Tubs.PurseSeineWellReconciliation>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(destination => destination.FormId, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                // Custom ignores
                .ForMember(destination => destination.ObserversTotal, opt => opt.Ignore())
                .ForMember(destination => destination.CumulativeTotal, opt => opt.Ignore())
                .ForMember(d => d.PortWell13, o => o.Ignore())
                .ForMember(d => d.PortWell14, o => o.Ignore())
                .ForMember(d => d.PortWell15, o => o.Ignore())
                .ForMember(d => d.PortWell16, o => o.Ignore())
                .ForMember(d => d.PortWell17, o => o.Ignore())
                .ForMember(d => d.PortWell18, o => o.Ignore())
                .ForMember(d => d.PortWell19, o => o.Ignore())
                .ForMember(d => d.PortWell20, o => o.Ignore())
                .ForMember(d => d.PortWell21, o => o.Ignore())
                .ForMember(d => d.PortWell22, o => o.Ignore())
                .ForMember(d => d.PortWell23, o => o.Ignore())
                .ForMember(d => d.PortWell24, o => o.Ignore())

                .ForMember(d => d.StarboardWell13, o => o.Ignore())
                .ForMember(d => d.StarboardWell14, o => o.Ignore())
                .ForMember(d => d.StarboardWell15, o => o.Ignore())
                .ForMember(d => d.StarboardWell16, o => o.Ignore())
                .ForMember(d => d.StarboardWell17, o => o.Ignore())
                .ForMember(d => d.StarboardWell18, o => o.Ignore())
                .ForMember(d => d.StarboardWell19, o => o.Ignore())
                .ForMember(d => d.StarboardWell20, o => o.Ignore())
                .ForMember(d => d.StarboardWell21, o => o.Ignore())
                .ForMember(d => d.StarboardWell22, o => o.Ignore())
                .ForMember(d => d.StarboardWell23, o => o.Ignore())
                .ForMember(d => d.StarboardWell24, o => o.Ignore())

                .ForMember(d => d.ObserverDate, o => o.MapFrom(s => s.GetDate()))
                .ForMember(d => d.LogsheetDate, o => o.MapFrom(s => s.GetLogDate()))
                // Backfill discrete values just because
                .ForMember(d => d.LogsheetDateOnly, o => o.MapFrom(s => s.LogDateOnly))
                .ForMember(d => d.LogsheetTimeOnly, o => o.MapFrom(s => s.LogTimeOnly))
                .ForMember(d => d.ActionCode, o => o.ResolveUsing<ActionTypeStringResolver>().FromMember(s => s.FillCode))
                ;
        }
    }
}

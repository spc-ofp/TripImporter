// -----------------------------------------------------------------------
// <copyright file="SetCatchProfile.cs" company="Secretariat of the Pacific Community">
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
    public class SetCatchProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.LonglineCatch, Tubs.LongLineCatch>()
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
                .ForMember(d => d.Spare1, o => o.Ignore())
                // Custom mapping               
                .ForMember(d => d.LandedConditionCode, o => o.MapFrom(s => s.ConditionCode))
                .AfterMap((s, d) =>
                {

                })
                ;
            
            
            CreateMap<Observer.PsSetCatch, Tubs.PurseSeineSetCatch>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.FishingSet, o => o.Ignore()) // Handled in SetProfile
                
                .ForMember(d => d.ContainsLargeFish, o => o.MapFrom(s => s.IsLargeFish))
                .ForMember(d => d.MetricTonsObserved, o => o.MapFrom(s => s.ObserverWeight))
                .ForMember(d => d.MetricTonsFromLog, o => o.MapFrom(s => s.VesselWeight))
                .ForMember(d => d.CountObserved, o => o.MapFrom(s => s.ObserverCount))
                .AfterMap((s,d) =>
                {
                    
                })
                ;
        }
    }
}

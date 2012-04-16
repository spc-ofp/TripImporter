// -----------------------------------------------------------------------
// <copyright file="PollutionDetailProfile.cs" company="Secretariat of the Pacific Community">
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
    using TubsCommon = Spc.Ofp.Tubs.DAL.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PollutionDetailProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.PollutionDetail, Tubs.PollutionDetail>()
                .Include<Observer.SpillDetail, Tubs.SpillDetail>()
                .Include<Observer.WasteDetail, Tubs.WasteDetail>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.PollutionType, o => o.Ignore())
                // Custom member mapping
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Type))
                ;

            CreateMap<Observer.SpillDetail, Tubs.SpillDetail>()
                .ForMember(d => d.PollutionType, o => o.UseValue(TubsCommon.PollutionType.SpillageOrLeakage))
                .ForMember(d => d.Source, o => o.ResolveUsing<SpillSourceResolver>().FromMember(s => s.Material));

            CreateMap<Observer.WasteDetail, Tubs.WasteDetail>()
                .ForMember(d => d.PollutionType, o => o.UseValue(TubsCommon.PollutionType.DumpedOverboard))
                .ForMember(d => d.Material, o => o.ResolveUsing<PollutionMaterialResolver>().FromMember(s => s.Material));
        }
    }
}

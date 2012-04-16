// -----------------------------------------------------------------------
// <copyright file="WellContentProfile.cs" company="Secretariat of the Pacific Community">
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
    public class WellContentProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.PsWellContent, Tubs.PurseSeineWellContent>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                
                // Mapping for fields with different names
                .ForMember(d => d.WellCapacity, o => o.MapFrom(s => s.Capacity))
                .ForMember(d => d.FuelOrWater, o => o.ResolveUsing<WellContentTypeResolver>().FromMember(s => s.WellContents))
                .ForMember(d => d.WellLocation, o => o.ResolveUsing<WellLocationResolver>().FromMember(s => s.PortOrStarboard))
                ;
        }
    }
}

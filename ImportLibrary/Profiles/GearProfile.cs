// -----------------------------------------------------------------------
// <copyright file="GearProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using System;
    using AutoMapper;
    using ImportLibrary.Resolvers;
    using Observer = Spc.Ofp.Legacy.Observer;
    using Tubs = Spc.Ofp.Tubs.DAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GearProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.Entities.PsFishingGear, Tubs.Entities.PurseSeineGear>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())

                .ForMember(destination => destination.PowerblockMake, opt => opt.Ignore())
                .ForMember(destination => destination.PurseWinchMake, opt => opt.Ignore())
                .ForMember(destination => destination.NetDepthInMeters, opt => opt.Ignore())
                .ForMember(destination => destination.NetLengthInMeters, opt => opt.Ignore())
                .ForMember(destination => destination.MeshSizeInCm, opt => opt.Ignore())
                .ForMember(d => d.BrailType, o => o.MapFrom(s => s.BrailDescription))
                .ForMember(d => d.NetDepthUnit, o => o.ResolveUsing<LengthUnitResolver>().FromMember(s => s.NetDepthUnits))
                .ForMember(d => d.NetLengthUnits, o => o.ResolveUsing<LengthUnitResolver>().FromMember(s => s.NetLengthUnits))
                .ForMember(d => d.MeshSizeUnits, o => o.ResolveUsing<LengthUnitResolver>().FromMember(s => s.MeshSizeUnits))
                ;
        }
    }
}

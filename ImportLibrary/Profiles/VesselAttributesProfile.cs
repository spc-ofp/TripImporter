// -----------------------------------------------------------------------
// <copyright file="VesselAttributesProfile.cs" company="Secretariat of the Pacific Community">
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
    public class VesselAttributesProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.PsVesselAttributes, Tubs.PurseSeineVesselAttributes>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                // Specific ignores
                .ForMember(destination => destination.AuxiliaryBoatCount, opt => opt.Ignore())
                .ForMember(destination => destination.LightCount, opt => opt.Ignore())
                .ForMember(d => d.VesselLength, o => o.Ignore())
                .ForMember(d => d.VesselLengthUnits, o => o.Ignore())
                .ForMember(d => d.VesselTonnage, o => o.Ignore())
                // Mapping for fields with different names
                .ForMember(d => d.HasTenderBoats, o => o.MapFrom(s => s.Tenderboats))
                .ForMember(d => d.SkiffMake, o => o.MapFrom(s => s.SkiffEngineMake))
                .ForMember(d => d.SkiffPower, o => o.MapFrom(s => s.SkiffEnginePower))
                .ForMember(d => d.CruiseSpeed, o => o.MapFrom(s => s.CruisingSpeed))
                .ForMember(d => d.HelicopterRangeUnit, o => o.ResolveUsing<LengthUnitResolver>().FromMember(s => s.HelicopterRangeUnits))
                .ForMember(d => d.HelicopterServiceOtherCount, o => o.MapFrom(s => s.HelicopterServiceCount))
                ;
        }
    }
}

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
                .ForMember(d => d.DctNotes, o => o.Ignore())
                .ForMember(d => d.DctScore, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
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

            CreateMap<Observer.Entities.LonglineFishingGear, Tubs.Entities.LongLineGear>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                .ForMember(d => d.DctNotes, o => o.Ignore())
                .ForMember(d => d.DctScore, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                // Comments/Usage are mixed up in the legacy system, but this will tease it out
                .ForMember(d => d.MainlineHaulerUsage, o => o.ResolveUsing<UsageCodeResolver>().FromMember(s => s.MainlineHaulerComments))
                .ForMember(d => d.MainlineHaulerComments, o => o.MapFrom(s => s.MainlineHaulerComments))
                .ForMember(d => d.BranchlineHaulerUsage, o => o.ResolveUsing<UsageCodeResolver>().FromMember(s => s.BranchlineHaulerComments))
                .ForMember(d => d.BranchlineHaulerComments, o => o.MapFrom(s => s.BranchlineHaulerComments))
                .ForMember(d => d.LineShooterUsage, o => o.ResolveUsing<UsageCodeResolver>().FromMember(s => s.LineShooterComments))
                .ForMember(d => d.LineShooterComments, o => o.MapFrom(s => s.LineShooterComments))
                .ForMember(d => d.BaitThrowerUsage, o => o.ResolveUsing<UsageCodeResolver>().FromMember(s => s.BaitThrowerComments))
                .ForMember(d => d.BaitThrowerComments, o => o.MapFrom(s => s.BaitThrowerComments))
                .ForMember(d => d.BranchlineAttacherUsage, o => o.ResolveUsing<UsageCodeResolver>().FromMember(s => s.BranchlineAttacherComments))
                .ForMember(d => d.BranchlineAttacherComments, o => o.MapFrom(s => s.BranchlineAttacherComments))
                .ForMember(d => d.HasWeightScales, o => o.MapFrom(s => s.HasWeighingScales))
                .ForMember(d => d.WeightScalesUsage, o => o.ResolveUsing<UsageCodeResolver>().FromMember(s => s.WeighingScalesComments))
                .ForMember(d => d.WeightScalesComments, o => o.MapFrom(s => s.WeighingScalesComments))
                // Data types changed from FoxPro to TUBS
                
                // This looks new to TUBS
                .ForMember(d => d.MainlineMaterialDescription, o => o.Ignore())
                .ForMember(d => d.BranchlineMaterial1Description, o => o.Ignore())
                .ForMember(d => d.BranchlineMaterial2Description, o => o.Ignore())
                .ForMember(d => d.BranchlineMaterial3Description, o => o.Ignore())
                .ForMember(d => d.OtherStorageDescription, o => o.Ignore())
                ;
        }
    }
}

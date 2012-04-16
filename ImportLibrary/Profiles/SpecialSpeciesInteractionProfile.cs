// -----------------------------------------------------------------------
// <copyright file="SpecialSpeciesInteractionProfile.cs" company="Secretariat of the Pacific Community">
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
    public class SpecialSpeciesInteractionProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.SsInteraction, Tubs.SpecialSpeciesInteraction>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                .ForMember(d => d.SightingDistanceInNm, o => o.Ignore())

                // Custom ignores
                .ForMember(d => d.EezId, o => o.Ignore())
                .ForMember(d => d.Details, o => o.Ignore())
                .ForMember(d => d.SgTime, o => o.Ignore())

                .ForMember(d => d.LandedDate, o => o.MapFrom(s => s.GetDate()))
                .ForMember(d => d.LandedDateOnly, o => o.MapFrom(s => s.DateOnly))
                .ForMember(d => d.LandedTimeOnly, o => o.MapFrom(s => s.TimeOnly.NullSafeRemoveColon()))
                .ForMember(d => d.SgType, o => o.ResolveUsing<SgTypeResolver>().FromMember(s => s.Landed))
                .ForMember(d => d.LandedConditionCode, o => o.ResolveUsing<ConditionCodeResolver>().FromMember(s => s.LandedConditionCode))
                .ForMember(d => d.DiscardedConditionCode, o => o.ResolveUsing<ConditionCodeResolver>().FromMember(s => s.DiscardedConditionCode))
                .ForMember(d => d.LandedLengthCode, o => o.ResolveUsing<LengthCodeResolver>().FromMember(s => s.LandedLengthCode))
                .ForMember(d => d.LandedSexCode, o => o.ResolveUsing<SexCodeResolver>().FromMember(s => s.LandedSex))
                .ForMember(d => d.InteractionId, o => o.ResolveUsing<InteractionActivityResolver>().FromMember(s => s.VesselActivity))
                .ForMember(d => d.InteractionOther, o => o.MapFrom(s => s.VesselActivityOther))
                .ForMember(d => d.InteractionActivity, o => o.ResolveUsing<InteractionActivityResolver>().FromMember(s => s.SightingActivity))
                .ForMember(d => d.InteractionIfOther, o => o.MapFrom(s => s.SightingActivityOther))

                .ForMember(d => d.SightingDistanceUnit, o => o.ResolveUsing<LengthUnitResolver>().FromMember(s => s.SightingDistanceUnit))
                // Use this paradigm to map parent/child relationships?
                /*
                .AfterMap((s,d) => {
                    foreach (var x in d.Foo)
                        x.Parent = d;
                })
                */
                ;
        }
    }
}

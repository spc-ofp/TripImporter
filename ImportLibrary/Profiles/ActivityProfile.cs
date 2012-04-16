// -----------------------------------------------------------------------
// <copyright file="ActivityProfile.cs" company="Secretariat of the Pacific Community">
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
    public class ActivityProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.PsDailyEvent, Tubs.PurseSeineActivity>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Payao, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.RowVersion, o => o.Ignore())
                .ForMember(d => d.Day, o => o.Ignore()) // Handled in SeaDayProfile
                .ForMember(d => d.ActivityType, o => o.ResolveUsing<ActivityTypeResolver>().FromMember(s => s.ActivityType))
                .ForMember(d => d.SchoolAssociation, o => o.ResolveUsing<AssociationTypeResolver>().FromMember(s => s.AssociationType))
                .ForMember(d => d.DetectionMethod, o => o.ResolveUsing<DetectionMethodResolver>().FromMember(s => s.DetectionType))
                .ForMember(d => d.SeaCode, o => o.ResolveUsing<SeaCodeResolver>().FromMember(s => s.SeaCondition))
                .ForMember(d => d.LocalTime, o => o.MapFrom(s => s.GetDate()))
                .ForMember(d => d.UtcTime, o => o.MapFrom(s => s.GetUtcDate()))
                .ForMember(d => d.EezCode, o => o.MapFrom(s => s.EezId))
                .AfterMap((s, d) =>
                {
                    // Only set the link when the activity is Fishing.  Otherwise, null out the child FishingSet
                    if (d.ActivityType.HasValue && TubsCommon.ActivityType.Fishing == d.ActivityType.Value)
                    {
                        d.FishingSet.Activity = d;
                    }
                    else
                    {
                        d.FishingSet = null;
                    }
                })
                ;
        }
    }
}

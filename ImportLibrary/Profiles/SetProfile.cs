﻿// -----------------------------------------------------------------------
// <copyright file="SetProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using ImportLibrary.ExtensionMethods;
    using ImportLibrary.Resolvers;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SetProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.LonglineFishingSet, Tubs.Entities.LongLineSet>()
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                // Custom ignores                
                .ForMember(d => d.SetDateOnly, o => o.Ignore()) // Handled in AfterMap
                .ForMember(d => d.SetTimeOnly, o => o.Ignore()) // Handled in AfterMap
                .ForMember(d => d.UtcSetDateOnly, o => o.Ignore()) // Handled in AfterMap
                .ForMember(d => d.UtcSetTimeOnly, o => o.Ignore()) // Handled in AfterMap
                .ForMember(d => d.IsTargetingSharks, o => o.Ignore()) // Handled in AfterMap
                .ForMember(d => d.IsTargetingTuna, o => o.Ignore()) // Handled in AfterMap
                .ForMember(d => d.IsTargetingSwordfish, o => o.Ignore()) // Handled in AfterMap
                .ForMember(d => d.BaitSpecies5Hooks, o => o.Ignore())
                .ForMember(d => d.NotesList, o => o.Ignore())
                .ForMember(d => d.Baskets, o => o.Ignore())
                .ForMember(d => d.DiaryPage, o => o.Ignore())
                .ForMember(d => d.TdrLength, o => o.Ignore())
                .ForMember(d => d.LineSettingSpeedMetersPerSecond, o => o.Ignore()) // Handled in AfterMap
                .ForMember(d => d.Gen3Events, o => o.Ignore())
                .ForMember(d => d.WasObserved, o => o.Ignore())
                // Custom mapping
                .ForMember(d => d.SetDate, o => o.MapFrom(s => s.GetDate()))
                .ForMember(d => d.UtcSetDate, o => o.MapFrom(s => s.GetUtcDate()))
                .ForMember(d => d.SetId, o => o.MapFrom(s => s.SetNumber))
                .ForMember(d => d.Details, o => o.MapFrom(s => s.SetDetails))
                .ForMember(d => d.LineSettingSpeedUnit, o => o.ResolveUsing<LineSettingSpeedResolver>().FromMember(s => s.LineSettingSpeedUnit))
                .AfterMap((s, d) =>
                {
                    // Fix date/time splits
                    d.SetDateOnly = d.SetDate.AtMidnight();
                    d.SetTimeOnly = d.SetDate.TimeOnly();
                    d.UtcSetDateOnly = d.UtcSetDate.AtMidnight();
                    d.UtcSetTimeOnly = d.UtcSetDate.TimeOnly();

                    // Turn single integer into 1 of 3 booleans
                    d.IsTargetingTuna = s.TargetSpeciesId.HasValue && 1 == s.TargetSpeciesId.Value;
                    d.IsTargetingSwordfish = s.TargetSpeciesId.HasValue && 2 == s.TargetSpeciesId.Value;
                    d.IsTargetingSharks = s.TargetSpeciesId.HasValue && 3 == s.TargetSpeciesId.Value;

                    // Fix LineSettingSpeedMetersPerSecond
                    if (d.LineSettingSpeed.HasValue &&  Tubs.Common.UnitOfMeasure.MetersPerSecond.Equals(d.LineSettingSpeedUnit))
                    {
                        d.LineSettingSpeedMetersPerSecond = d.LineSettingSpeed.Value;
                    }
                    else if (d.LineSettingSpeed.HasValue && Tubs.Common.UnitOfMeasure.Knots.Equals(d.LineSettingSpeedUnit))
                    {
                        d.LineSettingSpeedMetersPerSecond = Tubs.Common.MetricConversions.KnotsToMetersPerSecond(d.LineSettingSpeed.Value);
                    }

                    // Reinstate relationships
                    foreach (var sc in d.CatchList)
                    {
                        if (null == sc)
                            continue;

                        sc.FishingSet = d;
                    }
                    foreach (var evt in d.EventList)
                    {
                        if (null == evt)
                            continue;

                        evt.FishingSet = d;
                    }
                    foreach (var cfactor in d.ConversionFactors)
                    {
                        if (null == cfactor)
                            continue;

                        cfactor.FishingSet = d;
                    }

                    // Fill in start/end of set and start/end of haul
                    Tubs.Entities.LongLineSetHaulEvent.SetStartAndEnd(d.EventList);
                })
                ;
            
            CreateMap<Observer.PsFishingSet, Tubs.Entities.PurseSeineSet>()
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Activity, o => o.Ignore()) // Handled in ActivityProfile
                // Custom Ignores
                .ForMember(d => d.ContainsLargeTuna, o => o.Ignore())
                .ForMember(d => d.StartOfSetFromLog, o => o.Ignore())
                .ForMember(d => d.TonsOfSkipjackObserved, o => o.Ignore())
                .ForMember(d => d.TonsOfBigeyeObserved, o => o.Ignore())
                .ForMember(d => d.TonsOfYellowfinObserved, o => o.Ignore())
                .ForMember(d => d.LargeTunaPercentage, o => o.Ignore())
                .ForMember(d => d.TonsOfYellowfinAndBigeyeObserved, o => o.Ignore())
                .ForMember(d => d.TotalTunaAnswer, o => o.Ignore())
                .ForMember(d => d.PercentageOfTuna, o => o.Ignore())
                .ForMember(d => d.TonsOfTunaObserved2, o => o.Ignore())
                .ForMember(d => d.LargeSpeciesPercentage, o => o.Ignore())
                // Handled in AfterMap
                .ForMember(d => d.ContainsLargeYellowfin, o => o.Ignore())
                .ForMember(d => d.ContainsLargeBigeye, o => o.Ignore())

                
                // Combined Date/Time values plus time-only values
                .ForMember(d => d.SkiffOff, o => o.MapFrom(s => s.DateOnly.Combine(s.TimeOnly)))
                .ForMember(d => d.SkiffOffTimeOnly, o => o.MapFrom(s => s.TimeOnly))

                .ForMember(d => d.WinchOn, o => o.MapFrom(s => s.DateOnly.Combine(s.WinchOn)))
                .ForMember(d => d.WinchOnTimeOnly, o => o.MapFrom(s => s.WinchOn))

                .ForMember(d => d.RingsUp, o => o.MapFrom(s => s.DateOnly.Combine(s.RingsUp)))
                .ForMember(d => d.RingsUpTimeOnly, o => o.MapFrom(s => s.RingsUp))

                .ForMember(d => d.BeginBrailing, o => o.MapFrom(s => s.DateOnly.Combine(s.StartOfBrail)))
                .ForMember(d => d.BeginBrailingTimeOnly, o => o.MapFrom(s => s.StartOfBrail))

                .ForMember(d => d.EndBrailing, o => o.MapFrom(s => s.DateOnly.Combine(s.EndOfBrail)))
                .ForMember(d => d.EndBrailingTimeOnly, o => o.MapFrom(s => s.EndOfBrail))

                .ForMember(d => d.EndOfSet, o => o.MapFrom(s => s.DateOnly.Combine(s.Stop)))
                .ForMember(d => d.EndOfSetTimeOnly, o => o.MapFrom(s => s.Stop))

                // Checked and double-checked
                .ForMember(d => d.WeightOnboardObserved, o => o.MapFrom(s => s.ld_onboard))
                .ForMember(d => d.WeightOnboardFromLog, o => o.MapFrom(s => s.ld_ves_onb))
                .ForMember(d => d.RetainedTonnageObserved, o => o.MapFrom(s => s.ld_tonnage))
                .ForMember(d => d.RetainedTonnageFromLog, o => o.MapFrom(s => s.ld_ves_ton))
                .ForMember(d => d.VesselTonnageOnlyFromThisSet, o => o.MapFrom(s => s.IsFromThisSet))
                .ForMember(d => d.NewOnboardTotalObserved, o => o.MapFrom(s => s.ld_newonboard))
                .ForMember(d => d.NewOnboardTotalFromLog, o => o.MapFrom(s => s.ld_ves_new))
                .ForMember(d => d.TonsOfTunaObserved, o => o.MapFrom(s => s.tuna_catch))
                .ForMember(d => d.SumOfBrail1, o => o.MapFrom(s => s.ld_brails))
                .ForMember(d => d.SumOfBrail2, o => o.MapFrom(s => s.ld_brails2))
                .ForMember(d => d.TotalCatch, o => o.MapFrom(s => s.total_catch))
                .ForMember(d => d.LargeSpecies, o => o.MapFrom(s => s.b_sp_id))
                .ForMember(d => d.LargeSpeciesCount, o => o.MapFrom(s => s.b_nb_species))
                .ForMember(d => d.CatchList, o => o.MapFrom(s => s.SetCatchList))
                .ForMember(d => d.SamplingHeaders, o => o.MapFrom(s => s.LengthFrequencyHeaders))
                .ForMember(d => d.Activity, o => o.MapFrom(s => s.DailyEvent))
                .AfterMap((s,d) => 
                {
                    foreach (var sc in d.CatchList)
                    {
                        if (null == sc)
                            continue;

                        sc.FishingSet = d;
                    }
                    foreach (var sh in d.SamplingHeaders)
                    {
                        if (null == sh)
                            continue;

                        sh.Set = d;
                    }
                    d.ContainsLargeBigeye =
                        (s.LargeBigeyePercentage.HasValue && s.LargeBigeyePercentage.Value > 0) ||
                        (s.LargeBigeyeCount.HasValue && s.LargeBigeyeCount.Value > 0);
                    d.ContainsLargeYellowfin =
                        (s.LargeYellowfinPercentage.HasValue && s.LargeYellowfinPercentage.Value > 0) ||
                        (s.LargeYellowfinCount.HasValue && s.LargeYellowfinCount.Value > 0);
                })
                ;
        }
    }
}

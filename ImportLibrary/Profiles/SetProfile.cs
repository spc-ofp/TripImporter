// -----------------------------------------------------------------------
// <copyright file="SetProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using AutoMapper;
    using ImportLibrary.ExtensionMethods;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SetProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.PsFishingSet, Tubs.PurseSeineSet>()
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
                        sc.FishingSet = d;
                    }
                    foreach (var sh in d.SamplingHeaders)
                    {
                        sh.Set = d;
                    }
                })
                ;
        }
    }
}

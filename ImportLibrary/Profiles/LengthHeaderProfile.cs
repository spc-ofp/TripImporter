// -----------------------------------------------------------------------
// <copyright file="LengthHeaderProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using AutoMapper;
    using ImportLibrary.Resolvers;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LengthHeaderProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.PsLengthFrequencyHeader, Tubs.LengthSamplingHeader>()
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Comments, opt => opt.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Set, o => o.Ignore())
                // Custom ignores
                .ForMember(d => d.FormId, o => o.Ignore())
                .ForMember(d => d.Brails, o => o.Ignore()) // Problematic in FoxPro
                //
                .ForMember(d => d.BrailStartTime, o => o.MapFrom(s => s.FishingSet.StartOfBrail))
                .ForMember(d => d.BrailEndTime, o => o.MapFrom(s => s.FishingSet.EndOfBrail))
                .ForMember(d => d.SamplingProtocol, o => o.ResolveUsing<SamplingProtocolResolver>().FromMember(s => s.SampleType))
                .ForMember(d => d.SamplingProtocolComments, o => o.MapFrom(s => s.ProtocolComments))
                .ForMember(d => d.SumOfAllBrails, o => o.MapFrom(s => s.SumOfBrails))
                .ForMember(d => d.MeasuringInstrument, o => o.MapFrom(s => s.Measure)) // TODO Char to String
                .ForMember(d => d.BrailNumber, o => o.MapFrom(s => s.WhichBrail))
                .ForMember(d => d.Samples, o => o.MapFrom(s => s.Details))
                //.ForMember(d => d.Set, o => o.MapFrom(s => s.FishingSet))
                .ForMember(d => d.PageCount, o => o.MapFrom(s => s.TotalPages))
                .ForMember(d => d.TotalBrailCount, o => o.MapFrom(s => s.TotalBrails))
                .AfterMap((s,d) =>
                {
                    foreach (var sample in d.Samples)
                    {
                        sample.Header = d;
                    }
                })
                ;
        }
    }
}

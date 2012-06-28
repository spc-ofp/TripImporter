// -----------------------------------------------------------------------
// <copyright file="ConversionFactorProfile.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using System;
    using AutoMapper;
    using ImportLibrary.ExtensionMethods;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConversionFactorProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.LonglineConversionFactor, Tubs.LongLineConversionFactor>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.FishingSet, o => o.Ignore())
                // Custom ignores
                .ForMember(d => d.Comments, o => o.Ignore())
                .ForMember(d => d.WetfinWeight, o => o.Ignore())
                // Custom mapping
                .ForMember(d => d.Date, o => o.MapFrom(s => s.GetDate()))
                ;
        }
    }
}

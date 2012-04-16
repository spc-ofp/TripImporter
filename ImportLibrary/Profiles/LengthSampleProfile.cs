// -----------------------------------------------------------------------
// <copyright file="LengthSampleProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using System;
    using AutoMapper;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LengthSampleProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.PsLengthFrequencyDetail, Tubs.LengthSample>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Header, o => o.Ignore())
                //
                .ForMember(d => d.SequenceNumber, o => o.MapFrom(s => s.SampleNumber))
                ;
        }
    }
}

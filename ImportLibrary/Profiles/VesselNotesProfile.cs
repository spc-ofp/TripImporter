// -----------------------------------------------------------------------
// <copyright file="VesselNotesProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using AutoMapper;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class VesselNotesProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.Trip, Tubs.VesselNotes>()
                .ForMember(d => d.CaptainCountryCode, opt => opt.Ignore())
                .ForMember(d => d.MasterCountryCode, opt => opt.Ignore())
                ;
        }
    }
}

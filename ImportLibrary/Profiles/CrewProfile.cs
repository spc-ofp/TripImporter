// -----------------------------------------------------------------------
// <copyright file="CrewProfile.cs" company="Secretariat of the Pacific Community">
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
    public class CrewProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.PsCrewMember, Tubs.PurseSeineCrew>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                //.ForMember(d => d.Name, o => o.MapFrom(s => s.Name.NullSafeTrim()))
                .ForMember(d => d.CountryCode, o => o.MapFrom(s => s.Nationality))
                .ForMember(d => d.Job, o => o.ResolveUsing<JobResolver>().FromMember(s => s.Position))
                ;
        }

    }
}

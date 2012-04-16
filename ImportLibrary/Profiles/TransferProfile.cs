// -----------------------------------------------------------------------
// <copyright file="TransferProfile.cs" company="Secretariat of the Pacific Community">
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
    /// AutoMapper profile for GEN-1 Transfers
    /// </summary>
    public class TransferProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.FishTransfer, Tubs.Transfer>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Vessel, opt => opt.Ignore()) // Replace with lookup resolver
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                // Not present in FoxPro
                .ForMember(destination => destination.EezCode, opt => opt.Ignore())
                // Member properties
                .ForMember(d => d.VesselType, o => o.ResolveUsing<VesselTypeResolver>().FromMember(s => s.VesselType))
                .ForMember(d => d.ActionType, o => o.ResolveUsing<ActionTypeStringResolver>().FromMember(s => s.TransferType))
                .ForMember(d => d.TransferTime, o => o.MapFrom(s => s.GetDate()))
                .ForMember(d => d.VesselName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Ircs, o => o.MapFrom(s => s.CallSign))
                .ForMember(d => d.VesselFlag, o => o.MapFrom(s => s.FlagCode))
            ;
        }
    }
}

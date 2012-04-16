// -----------------------------------------------------------------------
// <copyright file="ElectronicDeviceProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using System;
    using AutoMapper;
    using ImportLibrary.Converters;
    using ImportLibrary.Resolvers;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ElectronicDeviceProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Observer.MarineDevice, Tubs.MarineDevice>().ConvertUsing<MarineDeviceTypeConverter>();
            
            CreateMap<Observer.Electronics, Tubs.ElectronicDevice>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                .ForMember(d => d.HowMany, o => o.Ignore())
                .ForMember(d => d.SealsIntact, o => o.Ignore())
                .ForMember(d => d.DeviceType, o => o.MapFrom(s => s.Device))
                .ForMember(d => d.IsInstalled, o => o.ResolveUsing<YesNoResolver>().FromMember(s => s.YesNo))
                .ForMember(d => d.Usage, o => o.ResolveUsing<UsageCodeResolver>().FromMember(s => s.Usage))
                ;
        }
    }
}

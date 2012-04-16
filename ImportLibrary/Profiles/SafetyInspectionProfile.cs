// -----------------------------------------------------------------------
// <copyright file="SafetyInspectionProfile.cs" company="Secretariat of the Pacific Community">
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
    public class SafetyInspectionProfile : Profile
    {
        private static char? SliceFirstChar(string value)
        {
            if (String.IsNullOrEmpty(value) || 0 == value.Trim().Length)
                return null;

            return value.Trim()[0];
        }
        
        protected override void Configure()
        {
            CreateMap<Observer.SafetyInspection, Tubs.SafetyInspection>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))                
                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                .ForMember(destination => destination.LifeRafts, opt => opt.Ignore()) // Read-only, not sure it's necessary
                // These are ignored because they're handled in an AfterMap()
                .ForMember(d => d.Epirb1, o => o.Ignore())
                .ForMember(d => d.Epirb2, o => o.Ignore())
                .ForMember(d => d.Raft1, o => o.Ignore())
                .ForMember(d => d.Raft2, o => o.Ignore())
                .ForMember(d => d.Raft3, o => o.Ignore())
                .ForMember(d => d.Raft4, o => o.Ignore())

                //
                .ForMember(d => d.LifejacketProvided, o => o.ResolveUsing<LifeJacketProvidedResolver>().FromMember(s => s.LifejacketProvided))
                .ForMember(d => d.LifejacketAvailability, o => o.ResolveUsing<LifejacketAvailabilityResolver>().FromMember(s => s.LifejacketsAvailable))
 
                // There's probably a smarter way to do this, but this works for me
                // NOTE:  Probably have to ignore these values too...
                .AfterMap((s, d) =>
                {
                    d.Epirb1 = new Tubs.EpirbResult()
                    {
                        BeaconType = s.Epi1Type.NullSafeTrim(),
                        Count = s.Epi2Count,
                    };
                    d.Epirb2 = new Tubs.EpirbResult()
                    {
                        BeaconType = s.Epi2Type.NullSafeTrim(),
                        Count = s.Epi2Count
                    };
                    d.Raft1 = new Tubs.RaftResult()
                    {
                        Capacity = s.CapacityOfLiferaft1,
                        InspectionDate = s.ExpiryOfLiferaft1,
                        LastOrDue = SliceFirstChar(s.TypeOfLiferaft1)
                    };
                    d.Raft2 = new Tubs.RaftResult()
                    {
                        Capacity = s.CapacityOfLiferaft2,
                        InspectionDate = s.ExpiryOfLiferaft2,
                        LastOrDue = SliceFirstChar(s.TypeOfLiferaft2)
                    };
                    d.Raft3 = new Tubs.RaftResult()
                    {
                        Capacity = s.CapacityOfLiferaft3,
                        InspectionDate = s.ExpiryOfLiferaft3,
                        LastOrDue = SliceFirstChar(s.TypeOfLiferaft3)
                    };
                    d.Raft4 = new Tubs.RaftResult()
                    {
                        Capacity = s.CapacityOfLiferaft4,
                        InspectionDate = s.ExpiryOfLiferaft4,
                        LastOrDue = SliceFirstChar(s.TypeOfLiferaft4)
                    };
                })
                ;
        }
    }
}

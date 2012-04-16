// -----------------------------------------------------------------------
// <copyright file="TripMonitorProfile.cs" company="Secretariat of the Pacific Community">
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
    public class TripMonitorProfile : Profile
    {
        protected Tubs.TripMonitorDetail BuildDetail(DateTime? time, string comment)
        {
            if (!time.HasValue || String.IsNullOrWhiteSpace(comment))
                return null;

            return new Tubs.TripMonitorDetail()
            {
                DetailDate = time,
                Comments = comment.NullSafeTrim(),
                EnteredBy = "TubsTripProcessor",
                EnteredDate = DateTime.Now
            };
        }


        protected override void Configure()
        {
            CreateMap<Observer.Gen3Form, Tubs.TripMonitor>()
                .ForMember(d => d.EnteredBy, o => o.UseValue("TubsTripProcessor"))
                .ForMember(d => d.EnteredDate, o => o.UseValue(DateTime.Now))

                // Standard ignores
                .ForMember(destination => destination.DctNotes, opt => opt.Ignore())
                .ForMember(destination => destination.DctScore, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedBy, opt => opt.Ignore())
                .ForMember(destination => destination.UpdatedDate, opt => opt.Ignore())
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(d => d.Trip, o => o.Ignore())
                // Managed via AfterMap
                .ForMember(d => d.Details, o => o.Ignore())
                // Luckily, Question1 through Question20 are the same on both sides
                .AfterMap((s, d) =>
                {
                    var detail = BuildDetail(s.Date1, s.Comment1);
                    if (null != detail)
                        d.AddDetail(detail);
                    detail = BuildDetail(s.Date2, s.Comment2);
                    if (null != detail)
                        d.AddDetail(detail);
                    detail = BuildDetail(s.Date3, s.Comment3);
                    if (null != detail)
                        d.AddDetail(detail);
                })
                ;
        }
    }
}

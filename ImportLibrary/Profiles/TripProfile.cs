// -----------------------------------------------------------------------
// <copyright file="TripProfile.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using ImportLibrary.Converters;
    using ImportLibrary.ExtensionMethods;
    using ImportLibrary.Resolvers;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TripProfile : Profile
    {
        protected void PostMapFixup(Tubs.Trip dest)
        {
            if (null != dest.Inspection)
                dest.Inspection.Trip = dest;

            if (null != dest.TripMonitor)
                dest.TripMonitor.Trip = dest;

            foreach (var sighting in dest.Sightings)
            {
                sighting.Trip = dest;
            }
            foreach (var transfer in dest.Transfers)
            {
                transfer.Trip = dest;
            }
            foreach (var pevent in dest.PollutionEvents)
            {
                pevent.Trip = dest;
            }

            // Remove any devices where the underlying MarineDevice is null
            var electronics = dest.Electronics.ToList();
            electronics.RemoveAll(x => x.DeviceType == null);
            dest.Electronics.Clear();

            foreach (var device in electronics)
            {
                device.Trip = dest;
                dest.Electronics.Add(device);
            }
            foreach (var interaction in dest.Interactions)
            {
                interaction.Trip = dest;
            }

            dest.DepartureDateOnly = dest.DepartureDate.AtMidnight();
            dest.DepartureTimeOnly = dest.DepartureDate.TimeOnly();
            dest.ReturnDateOnly = dest.ReturnDate.AtMidnight();
            dest.ReturnTimeOnly = dest.ReturnDate.TimeOnly();
        }
        
        
        protected override void Configure()
        {
            // Couple of notes:
            // http://odetocode.com/Blogs/scott/archive/2009/02/19/mapping-objects-with-automapper.aspx
            // http://blog.degree.no/2012/01/custom-mapping-no-i-use-automapper/
            // http://www.codeproject.com/Articles/61629/AutoMapper
            CreateMap<Observer.Vessel, Tubs.Vessel>().ConvertUsing<VesselTypeConverter>();
            CreateMap<Observer.FieldStaff, Tubs.Observer>().ConvertUsing<ObserverTypeConverter>();
            CreateMap<Observer.Port, Tubs.Port>().ConvertUsing<PortTypeConverter>();

            CreateMap<Observer.Trip, Tubs.Trip>()
                .Include<Observer.PurseSeineTrip, Tubs.PurseSeineTrip>()
                .Include<Observer.LongLineTrip, Tubs.LongLineTrip>()
                // Standard ignores
                .ForMember(d => d.DctNotes, opt => opt.Ignore())
                .ForMember(d => d.DctScore, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Vessel, opt => opt.Ignore()) // Huh?
                // Custom Ignores
                .ForMember(d => d.Pushpins, o => o.Ignore())
                .ForMember(d => d.CommunicationServices, o => o.Ignore())
                .ForMember(d => d.RowVersion, o => o.Ignore())
                .ForMember(d => d.WellCapacity, o => o.Ignore())
                .ForMember(d => d.CatchAndEffort, o => o.Ignore())
                .ForMember(d => d.MultiLandingInteractions, o => o.Ignore())
                .ForMember(d => d.PageCounts, o => o.Ignore())
                .ForMember(d => d.IsSpillSampled, o => o.Ignore())
                .ForMember(d => d.SpillTripNumber, o => o.Ignore())
                .ForMember(d => d.Gen3Answers, o => o.Ignore()) // New setup in TUBS
                .ForMember(d => d.Gen3Details, o => o.Ignore()) // New setup in TUBS
                .ForMember(d => d.ObserverDepartureLatitude, o => o.Ignore()) // Not present in FoxPro
                .ForMember(d => d.ObserverDepartureLongitude, o => o.Ignore()) // Not present in FoxPro
                .ForMember(d => d.ObserverReturnLatitude, o => o.Ignore()) // Not present in FoxPro
                .ForMember(d => d.ObserverReturnLongitude, o => o.Ignore()) // Not present in FoxPro
                .ForMember(d => d.CrewCount, o => o.Ignore()) // Not present as a single field in FoxPro
                // Handled in AfterMap
                .ForMember(d => d.DepartureDateOnly, o => o.Ignore())
                .ForMember(d => d.DepartureTimeOnly, o => o.Ignore())
                .ForMember(d => d.ReturnDateOnly, o => o.Ignore())
                .ForMember(d => d.ReturnTimeOnly, o => o.Ignore())
                .ForMember(d => d.UtcDepartureDate, o => o.Ignore())
                .ForMember(d => d.HasWasteDisposal, o => o.Ignore())
                .ForMember(d => d.WasteDisposalDescription, o => o.Ignore())
                // Members to copy
                .ForMember(d => d.ProgramCode, o => o.ResolveUsing<ProgramCodeResolver>().FromMember(s => s.ProgramId))
                .ForMember(d => d.TripMonitor, o => o.MapFrom(s => s.Gen3))
                .ForMember(d => d.Electronics, o => o.MapFrom(s => s.ElectronicDevices))
                .ForMember(d => d.Version, o => o.ResolveUsing<FormVersionResolver>().FromMember(s => s.FormVersion))
                .ForMember(d => d.VesselNotes, o => o.MapFrom(s => s))
                .ForMember(d => d.VesselDepartureDate, o => o.MapFrom(s => s.VesselDepartureDate))
                // It looks like parent/child relationships would be okay...
                // except that AutoMapper freaks out when one end or the other is an abstract class.
                .AfterMap((s, d) =>
                {
                    PostMapFixup(d);
                })
                ;

            CreateMap<Observer.LongLineTrip, Tubs.LongLineTrip>()
                .ForMember(d => d.Gear, o => o.MapFrom(s => s.FishingGear))
                .AfterMap((s,d) =>
                {
                    PostMapFixup(d);
                    if (null != d.Gear) { d.Gear.Trip = d; }
                    foreach (var fset in d.FishingSets)
                    {
                        fset.Trip = d;
                    }
                })
                ;
            
            CreateMap<Observer.PurseSeineTrip, Tubs.PurseSeineTrip>()
                .ForMember(d => d.Gear, o => o.MapFrom(s => s.FishingGear))
                .ForMember(d => d.SeaDays, o => o.MapFrom(s => s.Days))
                .ForMember(d => d.WellContent, o => o.MapFrom(s => s.WellContents))  
                .AfterMap((s,d) =>
                {
                    PostMapFixup(d);
                    if (null != d.Gear) { d.Gear.Trip = d; }
                    if (null != d.VesselAttributes) 
                    { 
                        d.VesselAttributes.Trip = d;
                    }
                    foreach (var day in d.SeaDays)
                    {
                        day.Trip = d;
                    }
                    d.Crew.ToList().ForEach(x => x.Trip = d);
                    foreach (var wc in d.WellContent)
                    {
                        wc.Trip = d;
                    }
                    foreach (var wr in d.WellReconciliations)
                    {
                        wr.Trip = d;
                    }

                    // FoxPro stores waste disposal data in VesselAttributes object
                    if (null != s.VesselAttributes)
                    {
                        d.HasWasteDisposal = s.VesselAttributes.HasWasteDisposal;
                        d.WasteDisposalDescription = s.VesselAttributes.WasteDisposalDescription;
                    }
                    
                    // Fix Set numbers
                    var sets =
                        from day in d.SeaDays
                        from activity in day.Activities
                        where activity.FishingSet != null
                        orderby activity.FishingSet.SkiffOff ascending
                        select activity.FishingSet;

                    var setNumbers =
                        from set in sets
                        select set.SetNumber;

                    // Only rework SetNumber if they're all zero
                    if (setNumbers.Sum() == 0)
                    {
                        int setNumber = 1;
                        foreach (var set in sets)
                        {
                            set.SetNumber = setNumber;
                            setNumber++;
                        }
                    }
                    
                })
                ;
        }
    }
}

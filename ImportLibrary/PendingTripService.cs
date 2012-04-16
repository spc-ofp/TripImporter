﻿// -----------------------------------------------------------------------
// <copyright file="PendingTripService.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using Spc.Ofp.Common.Repo;
    using Observer = Spc.Ofp.Legacy.Observer;
    using Tubs = Spc.Ofp.Tubs.DAL;
    using NHibernate;
    using log4net;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PendingTripService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PendingTripService));
        
        protected readonly ISession _sourceSession;

        public PendingTripService()
        {
            _sourceSession = Observer.DataService.GetSession();
        }

        public PendingTripService(ISession src)
        {
            _sourceSession = src;
        }


        private IList<TripViewModel> RemoveExisting(List<TripViewModel> trips)
        {
            using (var repo = new Tubs.TubsRepository<Tubs.Entities.TripHeader>(Tubs.TubsDataService.GetSession()))
            {
                var tripNumbers =
                    from trip in repo.All().ToList()
                    select trip.SpcTripNumber;
                HashSet<string> tubsTripNumbers = new HashSet<string>(tripNumbers);
                trips.RemoveAll(x => tubsTripNumbers.Contains(x.TripNumber));
            }
            return trips;
        }

        public Tuple<bool, string> CopyTrip(int sourceId)
        {
            bool success = false;
            string message = String.Empty;
            Tubs.Entities.Trip dest = null;
            
            var source = new Repository<Observer.Entities.Trip>(_sourceSession).FindBy(sourceId);
            Logger.DebugFormat("Found trip with Id {0}? {1}", sourceId, null != source);
            // Can't copy a trip you can't find
            if (null == source)
                return Tuple.Create(false, String.Format("Trip with Id {0} not found in source system", sourceId));
             
            // Temporary check
            Logger.DebugFormat("Trip type: {0}", source.GetType());
            if (typeof(Observer.Entities.PurseSeineTrip) != source.GetType())
                return Tuple.Create(false, String.Format("Trip of type {0} not yet supported", source.GetType()));
      
            try
            {
                // Map needs to happen within the scope of the session from the source database
                dest = Mapper.Map<Observer.Entities.Trip, Tubs.Entities.Trip>(source);
                Logger.Debug("Map operation completed");
            }
            catch (Exception ex)
            {
                Logger.Error("Map operation failed with Exception", ex);
                return Tuple.Create(false, ex.Message ?? "Unknown mapping error");
            }

            Logger.DebugFormat("Map operation was successful? {0}", null != dest);
            if (null == dest)
                return Tuple.Create(false, "Unknown mapping error:  Map produced null target entity");

            // Quick validation
            if (null == dest.Vessel || null == dest.DeparturePort || null == dest.ReturnPort || null == dest.Observer)
            {
                Logger.DebugFormat(
                    "Has Vessel? {0}\nHas Departure Port? {1}\nHas Return Port {2}\nHas Observer {3}",
                    null != dest.Vessel,
                    null != dest.DeparturePort,
                    null != dest.ReturnPort,
                    null != dest.Observer
                );
                return Tuple.Create(false, "Missing Vessel, Port(s), and/or Observer");
            }

            using (var session = Tubs.TubsDataService.GetSession())
            using (var xa = session.BeginTransaction())
            using (var statusRepo = new Tubs.TubsRepository<Tubs.Entities.ImportStatus>(session))
            {
                message = "Unknown error during save";
                try
                {
                    Tubs.TubsDataService.SaveFullTrip(dest);

                    var status = new Tubs.Entities.ImportStatus()
                    {
                        SourceName = "FoxPro Observer",
                        SourceId = sourceId.ToString(),
                        TripId = dest.Id,
                        EnteredBy = "TubsTripProcessor",
                        EnteredDate = DateTime.Now,
                        StatusCode = "S"
                    };
                    statusRepo.Add(status);
                    xa.Commit();
                    success = true;
                    message = String.Format("Trip saved with obstrip_id {0}", dest.Id);
                }
                catch (Exception ex)
                {
                    Logger.Error("Failed during trip save", ex);
                    message = ex.Message;
                }
            }
            
            return Tuple.Create(success, message);
        }
        
        
        public IList<TripViewModel> GetPendingTrips(string programCode = "All")
        {
            var pendingTrips = new List<TripViewModel>();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Spc.Ofp.Common.Repo.Repository<Observer.Entities.TripHeader>(session);

                bool IsAllPrograms =
                    String.IsNullOrEmpty(programCode) ||
                    "All".Equals(programCode, StringComparison.InvariantCultureIgnoreCase);

                // Default to All
                /*  Cool but unneccessary
                Expression<Func<Observer.Entities.TripHeader, bool>> filter = t => t.Id != 0;
                if (!IsAllPrograms)
                    filter = t => t.ProgramCode == programCode;
                */

                var query = IsAllPrograms ? repo.All() : repo.FilterBy(t => t.ProgramCode == programCode);

                pendingTrips = (
                    from trip in query.ToList()
                    select new TripViewModel
                    {
                        Id = trip.Id,
                        TripNumber = trip.SpcTripNumber,
                        ShouldCopy = true
                    }
                ).ToList();
            }
            return RemoveExisting(pendingTrips);
        }
    }
}
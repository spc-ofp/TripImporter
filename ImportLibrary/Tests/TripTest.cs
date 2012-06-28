// -----------------------------------------------------------------------
// <copyright file="TripTest.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Tests
{
    using System;
    using System.Linq;
    using AutoMapper;
    using NUnit.Framework;
    using Spc.Ofp.Common.Repo;
    using Observer = Spc.Ofp.Legacy.Observer;
    using Tubs = Spc.Ofp.Tubs.DAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class TripTest
    {
        [Test]
        public void GetLongLineTrip([Values(15850)] int tripId)
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.Trip>(session);
                var source = repo.FindBy(tripId);
                Assert.NotNull(source);
                Assert.IsInstanceOf<Observer.Entities.LongLineTrip>(source);
                var destination = Mapper.Map<Observer.Entities.Trip, Tubs.Entities.Trip>(source) as Tubs.Entities.LongLineTrip;
                Assert.NotNull(destination);
                Assert.NotNull(destination.Gear);
                Assert.NotNull(destination.Electronics);
                Assert.AreEqual(30, destination.Electronics.Count);
                // select count(*) from l_sethaul where obstrip_id = 15850
                // 9 entities
                Assert.NotNull(destination.FishingSets);
                Assert.AreEqual(9, destination.FishingSets.Count);

                var events =
                    from fset in destination.FishingSets
                    select fset.EventList.Count;
                Assert.AreEqual(96, events.Sum());

                var setcatch =
                    from fset in destination.FishingSets
                    select fset.CatchList.Count;
                Assert.AreEqual(900, setcatch.Sum());
            }
        }
        
        
        [Test]
        public void GetPurseSeineTrip([Values(11746)] int tripId)
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.Trip>(session);
                var source = repo.FindBy(tripId);
                Assert.NotNull(source);
                Assert.IsInstanceOf<Observer.Entities.PurseSeineTrip>(source);
                var destination = Mapper.Map<Observer.Entities.Trip, Tubs.Entities.Trip>(source) as Tubs.Entities.PurseSeineTrip;
                Assert.NotNull(destination);
                // TODO Pick some fields to check
                // NOTE:  Dig deep into the object graph.  Figure out how many days, sets, etc. this trip should have
                // select count(*) from s_alog where obstrip_id = 11746
                // 211 entities
                var activities =
                    from day in destination.SeaDays
                    select day.Activities.Count;
                Assert.AreEqual(211, activities.Sum());

                var fishingDays =
                    from day in destination.SeaDays
                    from activity in day.Activities
                    where activity != null && activity.FishDays.HasValue
                    select activity.FishDays;

                Assert.Greater(fishingDays.Sum(), 19.0);

                int totalSetCount = 0;
                int totalSetCatchCount = 0;
                foreach (var day in destination.SeaDays)
                {
                    
                    // Confirm link from day to trip
                    Assert.AreSame(destination, day.Trip);
                    foreach (var activity in day.Activities)
                    {
                        // Confirm link from activity to day
                        Assert.AreSame(day, activity.Day);
                        if (null != activity.FishingSet)
                        {
                            ++totalSetCount;
                            Assert.True(activity.FishingSet.SetNumber.HasValue);
                            Assert.False(0 == activity.FishingSet.SetNumber.Value);

                            totalSetCatchCount += activity.FishingSet.CatchList.Count;
                        }
                    }
                }

                Assert.AreEqual(14, totalSetCount);
                Assert.AreEqual(71, totalSetCatchCount);

                // select count(*) from s_day where obstrip_id = 11746
                // 24 entities
                Assert.True(24 == destination.SeaDays.Count);

                // Dig through electronics
                foreach (var device in destination.Electronics)
                {
                    Assert.NotNull(device);
                    Console.WriteLine("Make: {0}", device.Make);
                    Assert.NotNull(device.DeviceType);
                    Console.WriteLine("DeviceType: {0}\t{1}", device.DeviceType.Category, device.DeviceType.Description);
                }

            }
        }

        [Test]
        public void GetKiobTrip([Values(3900)] int tripId)
        {
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.Trip>(session);
                var source = repo.FindBy(tripId);
                Assert.NotNull(source);
                Assert.IsInstanceOf<Observer.Entities.PurseSeineTrip>(source);
                var destination = Mapper.Map<Observer.Entities.Trip, Tubs.Entities.Trip>(source) as Tubs.Entities.PurseSeineTrip;
                Assert.NotNull(destination);
            }
        }

        [Test]
        [Ignore("Turned off while I work out other issues")]
        public void CopyPurseSeineTrip([Values(3831)] int tripId)
        {
            Mapper.AssertConfigurationIsValid();
            var _service = new PendingTripService();
            var results = _service.CopyTrip(tripId);
            Assert.True(results.Item1, results.Item2);
        }

        [Test]
        public void CopyLongLineTrip([Values(15712)] int tripId)
        {
            Mapper.AssertConfigurationIsValid();
            var _service = new PendingTripService();
            var results = _service.CopyTrip(tripId);
            Assert.True(results.Item1, results.Item2);
        }
    }
}

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
        public void GetTrip()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.Trip>(session);
                var source = repo.FindBy(11746);
                Assert.NotNull(source);
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
        public void GetKiobTrip()
        {
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.Trip>(session);
                var source = repo.FindBy(3900);
                Assert.NotNull(source);
                var destination = Mapper.Map<Observer.Entities.Trip, Tubs.Entities.Trip>(source) as Tubs.Entities.PurseSeineTrip;
                Assert.NotNull(destination);
            }
        }

        [Test]
        //[Ignore("Turned off while I work out other issues")]
        public void CopyTrip()
        {
            Mapper.AssertConfigurationIsValid();
            var _service = new PendingTripService();
            var results = _service.CopyTrip(4623);
            Assert.True(results.Item1, results.Item2);
        }
    }
}

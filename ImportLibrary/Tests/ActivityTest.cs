// -----------------------------------------------------------------------
// <copyright file="ActivityTest.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Tests
{
    using System;
    using AutoMapper;
    using NUnit.Framework;
    using Spc.Ofp.Common.Repo;
    using Observer = Spc.Ofp.Legacy.Observer;
    using Tubs = Spc.Ofp.Tubs.DAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class ActivityTest
    {
        // activityId value by inspection
        [Test]
        public void GetActivity([Values(623024)] int activityId)
        {
            Mapper.AssertConfigurationIsValid();
            Console.WriteLine("Valid configuration...");
            using (var session = Observer.DataService.GetSession())
            {
                Console.WriteLine("Opened session");
                Assert.IsTrue(session.IsOpen);
                Console.WriteLine("Session is open");
                var repo = new Repository<Observer.Entities.PsDailyEvent>(session);
                Console.WriteLine("Created PsDailyEvent repository");
                var source = repo.FindBy(activityId); // From obstrip_id 11746
                Console.WriteLine("Checking if returned entity is null");
                Assert.NotNull(source);
                Console.WriteLine("Source entity not null, mapping with AutoMapper");
                var destination = Mapper.Map<Observer.Entities.PsDailyEvent, Tubs.Entities.PurseSeineActivity>(source);
                Assert.NotNull(destination);
                // TODO Pick some fields to check
            }
        }
    }
}

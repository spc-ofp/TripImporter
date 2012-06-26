// -----------------------------------------------------------------------
// <copyright file="CrewTest.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Tests
{
    using AutoMapper;
    using NUnit.Framework;
    using Spc.Ofp.Common.Repo;
    using Observer = Spc.Ofp.Legacy.Observer;
    using Tubs = Spc.Ofp.Tubs.DAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class CrewTest
    {
        // There will always be a special place for the number 455
        [Test]
        public void GetCrew([Values(455)] int crewId)
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.PsCrewMember>(session);
                var source = repo.FindBy(crewId);
                Assert.NotNull(source);
                var destination = Mapper.Map<Observer.Entities.PsCrewMember, Tubs.Entities.PurseSeineCrew>(source);
                Assert.NotNull(destination);
                StringAssert.AreEqualIgnoringCase("Wen Te Lang", destination.Name);
                StringAssert.AreEqualIgnoringCase("TW", destination.CountryCode);
                Assert.True(destination.Job.HasValue);
                Assert.AreEqual(Tubs.Common.JobType.DeckBoss, destination.Job.Value);
            }
        }
    }
}

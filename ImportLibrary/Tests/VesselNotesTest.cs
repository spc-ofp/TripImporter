// -----------------------------------------------------------------------
// <copyright file="VesselNotesTest.cs" company="Secretariat of the Pacific Community">
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
    public class VesselNotesTest
    {
        [Test]
        public void GetVesselNotes()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.Trip>(session);
                var source = repo.FindBy(572);
                Assert.NotNull(source);
                var destination = Mapper.Map<Observer.Entities.Trip, Tubs.Entities.VesselNotes>(source);
                Assert.NotNull(destination);
                StringAssert.AreEqualIgnoringCase("PNG-PH-524", destination.Licenses);
            }
        }
    }
}

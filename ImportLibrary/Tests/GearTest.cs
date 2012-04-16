// -----------------------------------------------------------------------
// <copyright file="GearTest.cs" company="Secretariat of the Pacific Community">
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
    public class GearTest
    {
        [Test]
        public void GetGear()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.PsFishingGear>(session);
                var source = repo.FindBy(2189); // obstrip_id 15736
                Assert.NotNull(source);
                var destination = Mapper.Map<Observer.Entities.PsFishingGear, Tubs.Entities.PurseSeineGear>(source);
                Assert.NotNull(destination);
                StringAssert.AreEqualIgnoringCase("MARINE HYDROTEC", destination.PowerblockModel);
                StringAssert.AreEqualIgnoringCase("MARCO WS454", destination.PurseWinchModel);
                Assert.AreEqual(240, destination.NetDepth);
                Assert.True(destination.NetDepthUnit.HasValue);
                Assert.AreEqual(Tubs.Common.UnitOfMeasure.Meters, destination.NetDepthUnit.Value);
            }
        }
    }
}

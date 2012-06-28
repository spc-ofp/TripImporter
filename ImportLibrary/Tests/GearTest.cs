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
    using Spc.Ofp.Tubs.DAL.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class GearTest
    {
        // gearId determined via inspection
        [Test]
        public void GetPurseSeineGear([Values(2198)] int gearId)
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.PsFishingGear>(session);
                var source = repo.FindBy(gearId); // obstrip_id 15736
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

        // gearId determined via inspection
        [Test]
        public void GetLongLineGear([Values(1370)] int gearId)
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.LonglineFishingGear>(session);
                var source = repo.FindBy(gearId); // obstrip_id 14895
                Assert.NotNull(source);
                var destination = Mapper.Map<Observer.Entities.LonglineFishingGear, Tubs.Entities.LongLineGear>(source);
                Assert.NotNull(destination);
                Assert.True(destination.HasMainlineHauler.HasValue && destination.HasMainlineHauler.Value);
                Assert.True(UsageCode.ALL.Equals(destination.MainlineHaulerUsage));
                Assert.True(destination.HasBranchlineHauler.HasValue);
                Assert.False(destination.HasBranchlineHauler.Value);
                StringAssert.AreEqualIgnoringCase("MONOFILAMENT", destination.MainlineMaterial);
                Assert.True(destination.MainlineLength.HasValue);
                Assert.AreEqual(38, destination.MainlineLength.Value);
            }
        }
    }
}

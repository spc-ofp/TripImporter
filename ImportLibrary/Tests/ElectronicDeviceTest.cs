// -----------------------------------------------------------------------
// <copyright file="ElectronicDeviceTest.cs" company="Secretariat of the Pacific Community">
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
    public class ElectronicDeviceTest
    {
        // Id value determined by inspection
        [Test]
        public void GetElectronicDevice([Values(31820)] int electronicsId)
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.Electronics>(session);
                var source = repo.FindBy(electronicsId); // From obstrip_id 7175
                Assert.NotNull(source);
                var destination = Mapper.Map<Observer.Entities.Electronics, Tubs.Entities.ElectronicDevice>(source);
                Assert.NotNull(destination);
                Assert.NotNull(destination.DeviceType);
                StringAssert.AreEqualIgnoringCase("KODEN", destination.Make);
                StringAssert.AreEqualIgnoringCase("KGP-913", destination.Model);
                Assert.True(destination.Usage.HasValue);
                Assert.AreEqual(Tubs.Common.UsageCode.ALL, destination.Usage.Value);
            }
        }
    }
}

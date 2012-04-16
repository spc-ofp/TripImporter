// -----------------------------------------------------------------------
// <copyright file="MarineDeviceTest.cs" company="Secretariat of the Pacific Community">
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
    public class MarineDeviceTest
    {
        [Test]
        public void GetMarineDevice()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.MarineDevice>(session);
                var source = repo.FindBy(72);
                Assert.NotNull(source);
                var destination = Mapper.Map<Observer.Entities.MarineDevice, Tubs.Entities.MarineDevice>(source);
                Assert.NotNull(destination);
                StringAssert.AreEqualIgnoringCase("WIND SPEED/DIRECTION GAUGE", destination.Description);
                StringAssert.AreEqualIgnoringCase("Wind Speed / Direction Gauge", destination.Category);
            }
        }
    }
}

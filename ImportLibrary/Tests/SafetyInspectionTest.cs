// -----------------------------------------------------------------------
// <copyright file="SafetyInspectionTest.cs" company="Secretariat of the Pacific Community">
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
    public class SafetyInspectionTest
    {
        [Test]
        public void GetSafetyInspection()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.SafetyInspection>(session);
                var src = repo.FindBy(1635); // obstrip_id 15118
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.SafetyInspection, Tubs.Entities.SafetyInspection>(src);
                Assert.NotNull(dest);
                Assert.True(dest.LifejacketProvided.HasValue);
                Assert.True(dest.LifejacketProvided.Value);
                Assert.True(dest.LifejacketSizeOk.HasValue);
                Assert.True(dest.LifejacketSizeOk.Value);
                StringAssert.AreEqualIgnoringCase("E", dest.LifejacketAvailability);
                Assert.NotNull(dest.Epirb1);
                StringAssert.AreEqualIgnoringCase("406", dest.Epirb1.BeaconType);
            }
        }
    }
}

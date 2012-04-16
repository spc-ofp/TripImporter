// -----------------------------------------------------------------------
// <copyright file="WellReconciliationTest.cs" company="Secretariat of the Pacific Community">
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
    public class WellReconciliationTest
    {
        [Test]
        public void GetWellReconciliation()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.PsWellReconciliation>(session);
                var src = repo.FindBy(13526);
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.PsWellReconciliation, Tubs.Entities.PurseSeineWellReconciliation>(src);
                Assert.NotNull(dest);
                Assert.True(dest.LogsheetDate.HasValue);
                StringAssert.AreEqualIgnoringCase("0215", dest.LogsheetTimeOnly);
                Assert.True(dest.ObserverDate.HasValue);
                StringAssert.AreEqualIgnoringCase("0214", dest.ObserverTimeOnly);
                Assert.True(dest.PortWell5.HasValue);
                Assert.AreEqual(28.0, dest.PortWell5.Value);
                Assert.True(dest.StarboardWell3.HasValue);
                Assert.AreEqual(37.0, dest.StarboardWell3.Value);
            }
        }
    }
}

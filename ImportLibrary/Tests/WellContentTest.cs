// -----------------------------------------------------------------------
// <copyright file="WellContentTest.cs" company="Secretariat of the Pacific Community">
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
    public class WellContentTest
    {
        [Test]
        public void GetWellContent()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.PsWellContent>(session);
                var src = repo.FindBy(1600); // obstrip_id 14938
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.PsWellContent, Tubs.Entities.PurseSeineWellContent>(src);
                Assert.NotNull(dest);
                Assert.True(dest.FuelOrWater.HasValue);
                Assert.AreEqual(Tubs.Common.WellContentType.Water, dest.FuelOrWater.Value);
                Assert.True(dest.WellNumber.HasValue);
                Assert.AreEqual(3, dest.WellNumber.Value);
                Assert.True(dest.WellCapacity.HasValue);
                Assert.AreEqual(45.0, dest.WellCapacity.Value);
            }
        }
    }
}

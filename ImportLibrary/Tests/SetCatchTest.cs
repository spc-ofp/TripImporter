// -----------------------------------------------------------------------
// <copyright file="SetCatchTest.cs" company="Secretariat of the Pacific Community">
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
    public class SetCatchTest
    {
        [Test]
        public void GetSetCatch()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.PsSetCatch>(session);
                var src = repo.FindBy(12345);
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.PsSetCatch, Tubs.Entities.PurseSeineSetCatch>(src);
                Assert.NotNull(dest);
            }
        }

        // catchId values determined by inspection
        // These are all female DOL that came on board in A1 condition and were caught Christmas day 2011
        [Test]
        public void GetLongLineCatch([Values(2515256, 2515246, 2515266, 2515258)] int catchId)
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.LonglineCatch>(session);
                var src = repo.FindBy(catchId);
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.LonglineCatch, Tubs.Entities.LongLineCatch>(src);
                Assert.NotNull(dest);
                Assert.IsNotNullOrEmpty(dest.SpeciesCode);
                Assert.AreEqual(Tubs.Common.ConditionCode.A1, dest.LandedConditionCode);
                Assert.AreEqual(Tubs.Common.SexCode.F, dest.SexCode);
                var xmas = new System.DateTime(2011, 12, 25);
                Assert.AreEqual(xmas, dest.DateOnly);
                Assert.IsNotNullOrEmpty(dest.TimeOnly);
            }
        }
    }
}

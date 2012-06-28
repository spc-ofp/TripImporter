// -----------------------------------------------------------------------
// <copyright file="SetTest.cs" company="Secretariat of the Pacific Community">
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
    public class SetTest
    {
        [Test]
        public void GetSet([Values(1116394)] int setId)
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.PsFishingSet>(session);
                var src = repo.FindBy(setId);
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.PsFishingSet, Tubs.Entities.PurseSeineSet>(src);
                Assert.NotNull(dest);
                Assert.NotNull(dest.SamplingHeaders);
                Assert.True(dest.SamplingHeaders.Count > 1);
                foreach (var header in dest.SamplingHeaders)
                {
                    Assert.NotNull(header);
                    StringAssert.AreEqualIgnoringCase("alberts", header.EnteredBy);
                    StringAssert.AreEqualIgnoringCase("T", header.MeasuringInstrument);
                }
            }
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="SetHaulTest.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Tests
{
    using System;
    using AutoMapper;
    using NUnit.Framework;
    using Spc.Ofp.Common.Repo;
    using Observer = Spc.Ofp.Legacy.Observer;
    using Tubs = Spc.Ofp.Tubs.DAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class SetHaulTest
    {
        // sethaulId values determined by inspection
        [Test]
        public void GetSetHaul([Values(63596, 63595, 62870, 60561, 59123, 50800)] int sethaulId)
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.LonglineFishingSet>(session);
                var src = repo.FindBy(sethaulId);
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.LonglineFishingSet, Tubs.Entities.LongLineSet>(src);
                Assert.NotNull(dest);
                if (dest.LineSettingSpeedUnit.HasValue)
                {
                    Assert.True(Tubs.Common.UnitOfMeasure.Knots.Equals(dest.LineSettingSpeedUnit.Value));
                }
                if (!String.IsNullOrEmpty(dest.TargetSpeciesCode))
                {
                    StringAssert.AreEqualIgnoringCase("ALB", dest.TargetSpeciesCode);
                }
                Assert.True(dest.TotalHookCount.HasValue);
                Assert.AreEqual(1830, dest.TotalHookCount.Value);
                Assert.True(dest.TotalBasketCount.HasValue);
                Assert.AreEqual(61, dest.TotalBasketCount);
                Assert.True(dest.IsTargetingTuna.HasValue && dest.IsTargetingTuna.Value);
            }
        }
    }
}

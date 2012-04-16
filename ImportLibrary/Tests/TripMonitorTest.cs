// -----------------------------------------------------------------------
// <copyright file="TripMonitorTest.cs" company="Secretariat of the Pacific Community">
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
    public class TripMonitorTest
    {
        [Test]
        public void GetGen3()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.Gen3Form>(session);
                var src = repo.FindBy(1514); // obstrip_id 15119
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.Gen3Form, Tubs.Entities.TripMonitor>(src);
                Assert.NotNull(dest);
                Assert.True(dest.Question5.HasValue);
                Assert.True(dest.Question5.Value);
                Assert.True(dest.Question4.HasValue);
                Assert.False(dest.Question4.Value);
                Assert.NotNull(dest.Details);
                Assert.True(dest.Details.Count > 0);
            }
        }
    }
}

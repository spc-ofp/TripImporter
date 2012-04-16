// -----------------------------------------------------------------------
// <copyright file="LengthSampleTest.cs" company="Secretariat of the Pacific Community">
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
    public class LengthSampleTest
    {
        [Test]
        public void GetLengthSample()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.PsLengthFrequencyHeader>(session);
                var source = repo.FindBy(66000);
                Assert.NotNull(source);
                var destination = Mapper.Map<Observer.Entities.PsLengthFrequencyHeader, Tubs.Entities.LengthSamplingHeader>(source);
                Assert.NotNull(destination);
                Assert.NotNull(destination.Samples);
                Assert.AreEqual(21.0, destination.SumOfAllBrails);
                Assert.AreEqual(30, destination.TotalBrailCount);
                Assert.AreEqual(11, destination.SevenEighthsBrailCount);
                Assert.AreEqual(7, destination.ThreeQuartersBrailCount);
            }
        }
    }
}

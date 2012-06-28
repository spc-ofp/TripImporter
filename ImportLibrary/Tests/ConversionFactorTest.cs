// -----------------------------------------------------------------------
// <copyright file="ConversionFactorTest.cs" company="Secretariat of the Pacific Community">
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
    using Spc.Ofp.Tubs.DAL.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class ConversionFactorTest
    {
        // factorId determined by inspection
        [Test]
        public void GetConversionFactors([Values(228618, 228620, 228621)] int factorId)
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.LonglineConversionFactor>(session);
                var source = repo.FindBy(factorId); // obstrip_id 10221
                Assert.NotNull(source);
                var destination = Mapper.Map<Observer.Entities.LonglineConversionFactor, Tubs.Entities.LongLineConversionFactor>(source);
                Assert.NotNull(destination);
                StringAssert.AreEqualIgnoringCase("swo", destination.SpeciesCode);
                Assert.True(destination.UfLength.HasValue);
                Assert.AreEqual(0, destination.UfLength.Value);
                Assert.IsNotNullOrEmpty(destination.ProcessedWeightCode);
                Assert.True(
                    "GH".Equals(destination.ProcessedWeightCode, StringComparison.InvariantCultureIgnoreCase) ||
                    "NM".Equals(destination.ProcessedWeightCode, StringComparison.InvariantCultureIgnoreCase));
                Assert.True(destination.LfLength.HasValue);
                Assert.True(destination.LfLength.Value > 75);
            }
        }
    }
}

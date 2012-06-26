// -----------------------------------------------------------------------
// <copyright file="SpecialSpeciesInteractionTest.cs" company="Secretariat of the Pacific Community">
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
    public class SpecialSpeciesInteractionTest
    {
        [Test]
        public void GetInteraction([Values(3915)] int interactionId)
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.SsInteraction>(session);
                var src = repo.FindBy(interactionId); // obstrip_id 15112
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.SsInteraction, Tubs.Entities.SpecialSpeciesInteraction>(src);
                Assert.NotNull(dest);
                StringAssert.AreEqualIgnoringCase("S", dest.SgType);
                StringAssert.AreEqualIgnoringCase("RTD", dest.SpeciesCode);
                StringAssert.AreEqualIgnoringCase("0817", dest.LandedTimeOnly);
                Assert.True(dest.LandedDate.HasValue);
                
                
            }
        }
    }
}

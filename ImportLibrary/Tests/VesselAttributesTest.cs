// -----------------------------------------------------------------------
// <copyright file="VesselAttributesTest.cs" company="Secretariat of the Pacific Community">
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
    public class VesselAttributesTest
    {
        [Test]
        public void GetVesselAttributes()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.PsVesselAttributes>(session);
                var src = repo.FindBy(2212); // obstrip_id 15735
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.PsVesselAttributes, Tubs.Entities.PurseSeineVesselAttributes>(src);
                Assert.NotNull(dest);

            }
        }
    }
}

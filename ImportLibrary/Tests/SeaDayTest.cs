// -----------------------------------------------------------------------
// <copyright file="SeaDayTest.cs" company="Secretariat of the Pacific Community">
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
    public class SeaDayTest
    {
        [Test]
        public void GetSeaDay()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.PsDay>(session);
                var src = repo.FindBy(12345);
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.PsDay, Tubs.Entities.PurseSeineSeaDay>(src);
                Assert.NotNull(dest);
            }
        }
    }
}

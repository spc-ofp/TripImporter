// -----------------------------------------------------------------------
// <copyright file="PollutionTest.cs" company="Secretariat of the Pacific Community">
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
    public class PollutionTest
    {
        [Test]
        public void GetPollutionEvent()
        {
            Mapper.AssertConfigurationIsValid();
            using (var session = Observer.DataService.GetSession())
            {
                var repo = new Repository<Observer.Entities.PollutionHeader>(session);
                var src = repo.FindBy(2588); // From obstrip_id 15013
                Assert.NotNull(src);
                var dest = Mapper.Map<Observer.Entities.PollutionHeader, Tubs.Entities.PollutionEvent>(src);
                Assert.NotNull(dest);
                Assert.NotNull(dest.Details);
            }
        }
    }
}

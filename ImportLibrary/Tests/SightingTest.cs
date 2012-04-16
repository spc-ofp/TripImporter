// -----------------------------------------------------------------------
// <copyright file="SightingTest.cs" company="Secretariat of the Pacific Community">
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
    public class SightingTest
    {
        [Test]
        public void GetSighting()
        {
            Mapper.AssertConfigurationIsValid();

            using (var session = Observer.DataService.GetSession())
            {
                Assert.NotNull(session);
                Assert.IsTrue(session.IsOpen);
                var repo = new Repository<Observer.Entities.Sighting>(session);
                var source = repo.FindBy(5);
                Assert.NotNull(source);
                var destination = Mapper.Map<Observer.Entities.Sighting, Tubs.Entities.Sighting>(source);
                Assert.NotNull(destination);
                // Pick some automapped fields and some explicitly mapped fields
                StringAssert.AreEqualIgnoringCase("0026.498S", destination.Latitude);
                StringAssert.AreEqualIgnoringCase("15342.319E", destination.Longitude);
                StringAssert.AreEqualIgnoringCase("FM", destination.EezCode);
                Assert.True(destination.VesselType.HasValue);
                Assert.AreEqual(Tubs.Common.SightedVesselType.SinglePurseSeine, destination.VesselType.Value);
                Assert.True(destination.ActionType.HasValue);
                Assert.AreEqual(Tubs.Common.ActionType.PF, destination.ActionType.Value);
            }
        }


        // Stuff to look at WRT NUnit goodness
        // http://lostechies.com/chrismissal/2010/12/22/automapper-tests-made-simple/
        /*
        [TestCase(null, null)] // passes objects to method in order
        public void Foo(string string1, string string2)
        {

        }
        */

        /*
        [TestCaseSource("GetTestData")]
        public TDestination Source_can_be_converted_to_expected_destination(TSource source)
        {
            var typeConverter = Activator.CreateInstance();
            return typeConverter.Convert(ResolutionContext.New(source));
        }
        */

        /*
         * 
         */
    }
}

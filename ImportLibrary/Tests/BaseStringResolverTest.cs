// -----------------------------------------------------------------------
// <copyright file="BaseStringResolverTest.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using ImportLibrary.Resolvers;
    using Tubs = Spc.Ofp.Tubs.DAL.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class BaseStringResolverTest
    {
        [Test]
        public void SexCode()
        {
            // NZOB, sourceId 1632, Set[0].CatchList[0].SexCode
            var result = BaseStringResolver.NullableEnumFromString(typeof(Tubs.SexCode), "X");
            Assert.IsNull(result);

        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="MapperSetup.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SetUpFixture]
    public class MapperSetup
    {
        [SetUp]
        public void LoadProfiles()
        {
            AutoMapperConfiguration.Configure();
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="WellLocationResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using AutoMapper;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class WellLocationResolver : ValueResolver<int?, string>
    {
        protected override string ResolveCore(int? source)
        {
            if (!source.HasValue)
                return null;

            // Based on the CleanStar function in DBF2TUBS
            switch (source.Value)
            {
                case 1:
                    return "P";
                case 2:
                    return "S";
                case 3:
                    return "B";
                case 4:
                    return "F";
                default:
                    return null;
            };
        }
    }
}

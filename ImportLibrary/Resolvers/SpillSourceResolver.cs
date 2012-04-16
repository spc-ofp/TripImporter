// -----------------------------------------------------------------------
// <copyright file="SpillSourceResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using System;
    using AutoMapper;
    using Tubs = Spc.Ofp.Tubs.DAL.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SpillSourceResolver : ValueResolver<string, Tubs.SpillSource?>
    {
        protected override Tubs.SpillSource? ResolveCore(string source)
        {
            if (String.IsNullOrEmpty(source) || 0 == source.Trim().Length)
                return null;

            // NOTE:  This only works for spills/leaks
            switch (source.Trim()[0])
            {
                case 'A':
                    return Tubs.SpillSource.VesselAgroundOrCollision;
                case 'B':
                    return Tubs.SpillSource.VesselAtAnchorOrBerth;
                case 'U':
                    return Tubs.SpillSource.VesselUnderway;
                case 'L':
                    return Tubs.SpillSource.LandBasedSource;
                case 'O':
                    return Tubs.SpillSource.Other;
                default:
                    return null;
            };
 
        }
    }
}

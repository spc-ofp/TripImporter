// -----------------------------------------------------------------------
// <copyright file="PollutionTypeResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using AutoMapper;
    using Tubs = Spc.Ofp.Tubs.DAL.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PollutionTypeResolver : ValueResolver<int?, Tubs.PollutionType?>
    {
        protected override Tubs.PollutionType? ResolveCore(int? source)
        {
            if (!source.HasValue)
                return null;

            switch (source.Value)
            {
                case 1:
                    return Tubs.PollutionType.DumpedOverboard;
                case 2:
                    return Tubs.PollutionType.SpillageOrLeakage;
                default:
                    return null;
            };
        }
    }
}

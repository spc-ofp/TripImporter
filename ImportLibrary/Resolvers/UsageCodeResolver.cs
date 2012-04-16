// -----------------------------------------------------------------------
// <copyright file="UsageCodeResolver.cs" company="Secretariat of the Pacific Community">
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
    public class UsageCodeResolver : ValueResolver<string, Tubs.UsageCode?>
    {
        protected override Tubs.UsageCode? ResolveCore(string source)
        {
            return (Tubs.UsageCode?)
                BaseStringResolver.NullableEnumFromString(typeof(Tubs.UsageCode), source);
        }
    }
}

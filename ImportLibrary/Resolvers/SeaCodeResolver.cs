// -----------------------------------------------------------------------
// <copyright file="SeaCodeResolver.cs" company="Secretariat of the Pacific Community">
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
    public class SeaCodeResolver : ValueResolver<string, Tubs.SeaCode?>
    {
        protected override Tubs.SeaCode? ResolveCore(string source)
        {
            return (Tubs.SeaCode?)
                BaseStringResolver.NullableEnumFromString(typeof(Tubs.SeaCode), source);
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="SexCodeResolver.cs" company="Secretariat of the Pacific Community">
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
    public class SexCodeResolver : ValueResolver<string, Tubs.SexCode?>
    {
        protected override Tubs.SexCode? ResolveCore(string source)
        {
            return (Tubs.SexCode?)
                BaseStringResolver.NullableEnumFromString(typeof(Tubs.SexCode), source);
        }
    }
}

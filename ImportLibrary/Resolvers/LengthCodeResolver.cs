// -----------------------------------------------------------------------
// <copyright file="LengthCodeResolver.cs" company="Secretariat of the Pacific Community">
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
    public class LengthCodeResolver : ValueResolver<string, Tubs.LengthCode?>
    {
        protected override Tubs.LengthCode? ResolveCore(string source)
        {
            return (Tubs.LengthCode?)
                BaseStringResolver.NullableEnumFromString(typeof(Tubs.LengthCode), source);
        }
    }
}

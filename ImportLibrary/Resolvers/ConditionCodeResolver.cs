// -----------------------------------------------------------------------
// <copyright file="ConditionCodeResolver.cs" company="Secretariat of the Pacific Community">
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
    public class ConditionCodeResolver : ValueResolver<string, Tubs.ConditionCode?>
    {
        protected override Tubs.ConditionCode? ResolveCore(string source)
        {
            return (Tubs.ConditionCode?)
                BaseStringResolver.NullableEnumFromString(typeof(Tubs.ConditionCode), source);
        }
    }
}

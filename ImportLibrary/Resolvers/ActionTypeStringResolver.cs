// -----------------------------------------------------------------------
// <copyright file="ActionTypeStringResolver.cs" company="Secretariat of the Pacific Community">
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
    public class ActionTypeStringResolver : ValueResolver<string, Tubs.ActionType?>
    {
        protected override Tubs.ActionType? ResolveCore(string source)
        {
            return (Tubs.ActionType?)
                BaseStringResolver.NullableEnumFromString(typeof(Tubs.ActionType), source);
        }
    }
}

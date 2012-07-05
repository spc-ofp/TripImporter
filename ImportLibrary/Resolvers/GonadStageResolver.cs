// -----------------------------------------------------------------------
// <copyright file="GonadStageResolver.cs" company="Secretariat of the Pacific Community">
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
    public class GonadStageResolver : ValueResolver<string, Tubs.GonadStage?>
    {
        protected override Tubs.GonadStage? ResolveCore(string source)
        {
            return (Tubs.GonadStage?)
                BaseStringResolver.NullableEnumFromString(typeof(Tubs.GonadStage), source);
        }
    }
}

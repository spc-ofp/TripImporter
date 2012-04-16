// -----------------------------------------------------------------------
// <copyright file="FormVersionResolver.cs" company="Secretariat of the Pacific Community">
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
    public class FormVersionResolver : ValueResolver<string, Tubs.WorkbookVersion?>
    {
        protected override Tubs.WorkbookVersion? ResolveCore(string source)
        {
            source = String.Format("v{0}", source.Trim());
            return (Tubs.WorkbookVersion?)
                BaseStringResolver.NullableEnumFromString(typeof(Tubs.WorkbookVersion), source, false);
        }
    }
}

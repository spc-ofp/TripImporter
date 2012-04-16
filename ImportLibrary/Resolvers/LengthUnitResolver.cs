// -----------------------------------------------------------------------
// <copyright file="LengthUnitResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using System;
    using AutoMapper;
    using Tubs = Spc.Ofp.Tubs.DAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LengthUnitResolver : ValueResolver<string, Tubs.Common.UnitOfMeasure?>
    {
        protected override Tubs.Common.UnitOfMeasure? ResolveCore(string source)
        {
            if (String.IsNullOrEmpty(source) || 0 == source.Trim().Length)
                return null;

            // FoxPro likes to append blank space to string fields
            source = source.Trim();

            if ("Fat".Equals(source, StringComparison.InvariantCultureIgnoreCase))
                return Tubs.Common.UnitOfMeasure.Fathoms;

            if ("Mtr".Equals(source, StringComparison.InvariantCultureIgnoreCase))
                return Tubs.Common.UnitOfMeasure.Meters;

            if ("Yrd".Equals(source, StringComparison.InvariantCultureIgnoreCase))
                return Tubs.Common.UnitOfMeasure.Yards;

            if ("cm".Equals(source, StringComparison.InvariantCultureIgnoreCase))
                return Tubs.Common.UnitOfMeasure.Centimeters;

            if ("in".Equals(source, StringComparison.InvariantCultureIgnoreCase))
                return Tubs.Common.UnitOfMeasure.Inches;

            return null;
        }
    }
}

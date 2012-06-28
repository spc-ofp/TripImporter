// -----------------------------------------------------------------------
// <copyright file="LineSettingSpeedResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using System;
    using AutoMapper;
    using Tubs = Spc.Ofp.Tubs.DAL.Common;

    /// <summary>
    /// Convert legacy integer value to TUBS Enumeration
    /// 1 = m/s, 2 = kts
    /// </summary>
    public class LineSettingSpeedResolver : ValueResolver<int?, Tubs.UnitOfMeasure?>
    {
        protected override Tubs.UnitOfMeasure? ResolveCore(int? source)
        {
            if (!source.HasValue)
                return null;

            if (1 == source.Value)
                return Tubs.UnitOfMeasure.MetersPerSecond;

            if (2 == source.Value)
                return Tubs.UnitOfMeasure.Knots;

            return null;
        }
    }
}

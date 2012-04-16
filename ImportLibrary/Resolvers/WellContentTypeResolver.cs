// -----------------------------------------------------------------------
// <copyright file="WellContentTypeResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using AutoMapper;
    using Observer = Spc.Ofp.Legacy.Observer.Common;
    using Tubs = Spc.Ofp.Tubs.DAL.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class WellContentTypeResolver : ValueResolver<Observer.WellContentType, Tubs.WellContentType?>
    {
        protected override Tubs.WellContentType? ResolveCore(Observer.WellContentType source)
        {
            switch (source)
            {
                case Observer.WellContentType.Fuel:
                    return Tubs.WellContentType.Fuel;
                case Observer.WellContentType.Water:
                    return Tubs.WellContentType.Water;
                case Observer.WellContentType.Other:
                    return Tubs.WellContentType.Other;
                default:
                    return null;
            }
        }
    }
}

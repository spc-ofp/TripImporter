// -----------------------------------------------------------------------
// <copyright file="VesselTypeResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using AutoMapper;
    using Tubs = Spc.Ofp.Tubs.DAL.Common;

    /// <summary>
    /// 
    /// </summary>
    public class VesselTypeResolver : ValueResolver<int?, Tubs.VesselType?>
    {
        protected override Tubs.VesselType? ResolveCore(int? source)
        {
            if (!source.HasValue)
                return null;

            // IMPORTANT!!!!!!!!
            // These values are based on the corrupted vessel type IDs
            // present in the SQLite copy
            // When using corrected data, change these to match
            // the new values (e.g. Light Aircraft = 21)
            switch (source.Value)
            {
                case 1:
                    return Tubs.VesselType.SinglePurseSeine;
                case 2:
                    return Tubs.VesselType.Trawler;
                case 3:
                    return Tubs.VesselType.Other;
                case 4:
                    return Tubs.VesselType.Longline;
                case 5:
                    return Tubs.VesselType.LightAircraft;
                case 6:
                    return Tubs.VesselType.Helicopter;
                case 7:
                    return Tubs.VesselType.PoleAndLine;
                case 8:
                    return Tubs.VesselType.Other;
                case 9:
                    return Tubs.VesselType.Mothership;
                case 10:
                    return Tubs.VesselType.Troll;
                case 11:
                    return Tubs.VesselType.NetBoat;
                case 12:
                    return Tubs.VesselType.Bunker;
                case 13:
                    return Tubs.VesselType.SearchAnchorOrLightBoat;
                case 14:
                    return Tubs.VesselType.FishCarrier;
                default:
                    return null;
            }
        }
    }
}

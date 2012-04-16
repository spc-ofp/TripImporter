// -----------------------------------------------------------------------
// <copyright file="SightedVesselResolver.cs" company="Secretariat of the Pacific Community">
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
    public class SightedVesselResolver : ValueResolver<int?, Tubs.SightedVesselType?>
    {
        protected override Tubs.SightedVesselType? ResolveCore(int? source)
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
                    return Tubs.SightedVesselType.SinglePurseSeine;
                case 2:
                    return Tubs.SightedVesselType.Trawler;
                case 3:
                    return Tubs.SightedVesselType.Other;
                case 4:
                    return Tubs.SightedVesselType.Longline;
                case 5:
                    return Tubs.SightedVesselType.LightAircraft;
                case 6:
                    return Tubs.SightedVesselType.Helicopter;
                case 7:
                    return Tubs.SightedVesselType.PoleAndLine;
                case 8:
                    return Tubs.SightedVesselType.Other;
                case 9:
                    return Tubs.SightedVesselType.Mothership;
                case 10:
                    return Tubs.SightedVesselType.Troll;
                case 11:
                    return Tubs.SightedVesselType.NetBoat;
                case 12:
                    return Tubs.SightedVesselType.Bunker;
                case 13:
                    return Tubs.SightedVesselType.SearchAnchorOrLightBoat;
                case 14:
                    return Tubs.SightedVesselType.FishCarrier;
                default:
                    return null;
            }
        }
    }
}

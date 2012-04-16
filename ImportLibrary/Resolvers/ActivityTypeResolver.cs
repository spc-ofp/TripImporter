// -----------------------------------------------------------------------
// <copyright file="ActivityTypeResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using AutoMapper;
    using Tubs = Spc.Ofp.Tubs.DAL.Common;

    /// <summary>
    /// Convert a nullable integer into a TUBS enumeration value.
    /// I couldn't find the source mapping in the FoxPro code and so had
    /// to use a different converter (DBF2TUBS.accdb).
    /// - CLC
    /// </summary>
    public class ActivityTypeResolver : ValueResolver<int?, Tubs.ActivityType?>
    {
        protected override Tubs.ActivityType? ResolveCore(int? source)
        {
            if (!source.HasValue)
                return null;

            switch (source.Value)
            {
                case 1:
                    return Tubs.ActivityType.Fishing;
                case 2:
                    return Tubs.ActivityType.Searching;
                case 3:
                    return Tubs.ActivityType.Transit;
                case 4:
                    return Tubs.ActivityType.NoFishingBreakdown;
                case 5:
                    return Tubs.ActivityType.NoFishingBadWeather;
                case 6:
                    return Tubs.ActivityType.InPort;
                case 7:
                    return Tubs.ActivityType.NetCleaningSet;
                case 8:
                    return Tubs.ActivityType.InvestigateFreeSchool;
                case 9:
                    return Tubs.ActivityType.InvestigateFloatingObject;
                case 10:
                case 23:
                    return Tubs.ActivityType.DeployFad;
                case 11:
                    return Tubs.ActivityType.NoFishingDriftingAtDaysEnd;
                case 12:
                    return Tubs.ActivityType.NoFishingDriftingWithFloatingObject;
                case 13:
                    return Tubs.ActivityType.NoFishingOther;
                case 14:
                    return Tubs.ActivityType.DriftingWithLights;
                case 16:
                    return Tubs.ActivityType.TransshippingOrBunkering;
                case 21:
                    return Tubs.ActivityType.HelicopterTakesOffToSearch;
                case 22:
                    return Tubs.ActivityType.HelicopterReturnsFromSearch;
                case 24:
                    return Tubs.ActivityType.RetrieveFad;
                case 25:
                    return Tubs.ActivityType.RetrieveRadioBuoy;
                case 26:
                    return Tubs.ActivityType.DeployRadioBuoy;
                case 94:
                    return Tubs.ActivityType.TransshippingAtSea;
                default:
                    return null;

            }
        }
    }
}

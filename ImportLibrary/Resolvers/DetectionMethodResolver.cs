// -----------------------------------------------------------------------
// <copyright file="DetectionMethodResolver.cs" company="Secretariat of the Pacific Community">
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
    public class DetectionMethodResolver : ValueResolver<int?, Tubs.DetectionMethod?>
    {
        protected override Tubs.DetectionMethod? ResolveCore(int? source)
        {
            if (!source.HasValue)
                return null;

            switch (source.Value)
            {
                case 1:
                    return Tubs.DetectionMethod.SeenFromVessel;
                case 2:
                    return Tubs.DetectionMethod.SeenFromHelicopter;
                case 3:
                    return Tubs.DetectionMethod.MarkedWithBeacon;
                case 4:
                    return Tubs.DetectionMethod.BirdRadar;
                case 5:
                    return Tubs.DetectionMethod.Sonar;
                case 6:
                    return Tubs.DetectionMethod.InfoFromOtherVessel;
                case 7:
                    return Tubs.DetectionMethod.Anchored;
                default:
                    return null;
            };
        }
    }
}

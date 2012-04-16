// -----------------------------------------------------------------------
// <copyright file="PollutionMaterialResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using System;
    using AutoMapper;
    using Tubs = Spc.Ofp.Tubs.DAL.Common;

    /// <summary>
    /// 
    /// </summary>
    public class PollutionMaterialResolver : ValueResolver<string, Tubs.PollutionMaterial?>
    {
        protected override Tubs.PollutionMaterial? ResolveCore(string source)
        {
            if (String.IsNullOrEmpty(source) || 0 == source.Trim().Length)
                return null;

            // NOTE:  This only works for materials dumped overboard
            switch (source.Trim()[0])
            {
                case 'P':
                    return Tubs.PollutionMaterial.Plastics;
                case 'M':
                    return Tubs.PollutionMaterial.Metals;
                case 'W':
                    return Tubs.PollutionMaterial.WasteOils;
                case 'C':
                    return Tubs.PollutionMaterial.Chemicals;
                case 'F':
                    return Tubs.PollutionMaterial.OldFishingGear;
                case 'G':
                    return Tubs.PollutionMaterial.GeneralGarbage;
                default:
                    return null;
            };
        }
    }
}

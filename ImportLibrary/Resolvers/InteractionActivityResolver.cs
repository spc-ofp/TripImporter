// -----------------------------------------------------------------------
// <copyright file="InteractionActivityResolver.cs" company="Secretariat of the Pacific Community">
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
    public class InteractionActivityResolver : ValueResolver<int?, Tubs.InteractionActivity?>
    {
        protected override Tubs.InteractionActivity? ResolveCore(int? source)
        {
            if (!source.HasValue)
                return null;

            switch (source.Value)
            {
                case 1:
                    return Tubs.InteractionActivity.Setting;
                case 2:
                    return Tubs.InteractionActivity.Hauling;
                case 3:
                    return Tubs.InteractionActivity.Transit;
                case 4:
                    return Tubs.InteractionActivity.Other;
                default:
                    return null;
            };
        }
    }
}

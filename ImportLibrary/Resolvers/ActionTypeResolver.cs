// -----------------------------------------------------------------------
// <copyright file="ActionTypeResolver.cs" company="Secretariat of the Pacific Community">
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
    public class ActionTypeResolver : ValueResolver<int?, Tubs.ActionType?>
    {
        protected override Tubs.ActionType? ResolveCore(int? source)
        {
            if (!source.HasValue)
                return null;

            switch (source.Value)
            {
                case 1:
                    return Tubs.ActionType.FI;
                case 2:
                    return Tubs.ActionType.PF;
                case 3:
                    return Tubs.ActionType.NF;
                case 4:
                    return Tubs.ActionType.DF;
                case 5:
                case 13:
                    return Tubs.ActionType.TG;
                case 6:
                case 14:
                    return Tubs.ActionType.SG;
                case 7:
                case 15:
                    return Tubs.ActionType.BG;
                case 8:
                case 16:
                    return Tubs.ActionType.OG;
                case 9:
                    return Tubs.ActionType.TR;
                case 10:
                    return Tubs.ActionType.SR;
                case 11:
                    return Tubs.ActionType.BR;
                case 12:
                    return Tubs.ActionType.OR;
                default:
                    return null;
            };
        }
    }
}

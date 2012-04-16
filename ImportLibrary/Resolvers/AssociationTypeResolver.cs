// -----------------------------------------------------------------------
// <copyright file="AssociationTypeResolver.cs" company="Secretariat of the Pacific Community">
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
    public class AssociationTypeResolver : ValueResolver<int?, Tubs.SchoolAssociation?>
    {
        protected override Tubs.SchoolAssociation? ResolveCore(int? source)
        {
            if (!source.HasValue)
                return null;

            switch (source.Value)
            {
                case 1:
                    return Tubs.SchoolAssociation.Unassociated;
                case 2:
                    return Tubs.SchoolAssociation.FeedingOnBaitfish;
                case 3:
                    return Tubs.SchoolAssociation.DriftingLog;
                case 4:
                    return Tubs.SchoolAssociation.DriftingRaft;
                case 5:
                    return Tubs.SchoolAssociation.AnchoredRaft;
                case 6:
                    return Tubs.SchoolAssociation.LiveWhale;
                case 7:
                    return Tubs.SchoolAssociation.LiveWhaleShark;
                case 8:
                    return Tubs.SchoolAssociation.Other;
                case 9:
                    // Allegedly, only on the old forms
                    return Tubs.SchoolAssociation.WhaleWhaleSharkPorpoise;
                default:
                    return null;
            };
        }
    }
}

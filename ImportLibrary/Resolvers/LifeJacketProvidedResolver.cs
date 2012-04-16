// -----------------------------------------------------------------------
// <copyright file="LifeJacketProvidedResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using AutoMapper;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LifeJacketProvidedResolver : ValueResolver<int?, bool?>
    {
        protected override bool? ResolveCore(int? source)
        {
            if (!source.HasValue)
                return null;

            switch (source.Value)
            {
                case 1:
                    return true;
                case 2:
                    return false;
                default:
                    return null;
            }
        }
    }
}

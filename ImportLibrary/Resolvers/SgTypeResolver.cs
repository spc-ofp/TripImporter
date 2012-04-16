// -----------------------------------------------------------------------
// <copyright file="SgTypeResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using AutoMapper;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SgTypeResolver : ValueResolver<int?, string>
    {
        protected override string ResolveCore(int? source)
        {
            if (!source.HasValue)
                return null;

            switch (source.Value)
            {
                case 1:
                    return "L";
                case 2:
                    return "I";
                case 3:
                    return "S";
                default:
                    return null;
            };
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="YesNoResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using System;
    using AutoMapper;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class YesNoResolver : ValueResolver<string, bool?>
    {
        protected override bool? ResolveCore(string source)
        {
            if (String.IsNullOrEmpty(source) || 0 == source.Trim().Length)
                return null;

            switch (source[0])
            {
                case 'Y':
                case 'y':
                    return true;
                case 'N':
                case 'n':
                    return false;
                default:
                    return null;
            }
        }
    }
}

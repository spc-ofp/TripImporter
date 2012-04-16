// -----------------------------------------------------------------------
// <copyright file="BaseStringResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class BaseStringResolver
    {
        public static object NullableEnumFromString(Type enumType, string source, bool caseInsensitive = true)
        {
            if (String.IsNullOrEmpty(source) || 0 == source.Trim().Length)
                return null;

            // Probably two passes through the BCL, but this looks much cleaner
            // in source code form
            source = source.Trim();
            if (caseInsensitive)
                source = source.ToUpper();
            if (!Enum.IsDefined(enumType, source))
                return null;

            return Enum.Parse(enumType, source);
        }
    }
}

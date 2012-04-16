// -----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class StringExtensions
    {
        public static string NullSafeTrim(this string value)
        {
            if (null == value)
                return value;
            return value.Trim();
        }

        public static string NullSafeRemoveColon(this string value)
        {
            if (null == value)
                return value;

            return value.Replace(":", String.Empty);
        }
    }
}

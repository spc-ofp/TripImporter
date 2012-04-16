// -----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.ExtensionMethods
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class DateTimeExtensions
    {
        public static DateTime? AtMidnight(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return null;

            return dateTime.Value.Subtract(dateTime.Value.TimeOfDay);
        }

        public static string TimeOnly(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return null;

            return dateTime.Value.ToString("HHmm");
        }
        
        
        public static DateTime? Combine(this DateTime? dateOnly, string timeOnly)
        {
            if (!dateOnly.HasValue)
            {
                return null;
            }

            DateTime fullDate = new DateTime(dateOnly.Value.Ticks);
            if (null != timeOnly)
            {
                if (5 == timeOnly.Trim().Length && timeOnly.Contains(":"))
                    timeOnly = timeOnly.Replace(":", String.Empty);
                if (4 == timeOnly.Trim().Length)
                {
                    double hours = 0;
                    if (Double.TryParse(timeOnly.Substring(0, 2), out hours))
                    {
                        System.Diagnostics.Debug.WriteLine("hours: " + hours);
                        fullDate = fullDate.AddHours(hours);
                    }
                    double minutes = 0;
                    if (Double.TryParse(timeOnly.Substring(2, 2), out minutes))
                    {
                        fullDate = fullDate.AddMinutes(minutes);
                    }
                }
            }
            return fullDate;
        }
    }
}

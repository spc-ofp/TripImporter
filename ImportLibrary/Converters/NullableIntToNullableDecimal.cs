// -----------------------------------------------------------------------
// <copyright file="NullableIntToNullableDecimal.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Converters
{
    using System;
    using AutoMapper;

    /// <summary>
    /// Very simple Int32 to Decimal conversion
    /// </summary>
    public class NullableIntToNullableDecimal : ITypeConverter<int?, decimal?>
    {
        public decimal? Convert(ResolutionContext context)
        {
            var source = context.SourceValue as int?;
            if (!source.HasValue)
                return null;
            return (decimal)source.Value;
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="SamplingProtocolResolver.cs" company="Secretariat of the Pacific Community">
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
    public class SamplingProtocolResolver : ValueResolver<int?, Tubs.SamplingProtocol>
    {
        protected override Tubs.SamplingProtocol ResolveCore(int? source)
        {
            if (!source.HasValue)
                return Tubs.SamplingProtocol.Other;

            switch (source.Value)
            {
                case 1:
                    return Tubs.SamplingProtocol.Normal;
                default:
                    return Tubs.SamplingProtocol.Other;
            }
        }
    }
}

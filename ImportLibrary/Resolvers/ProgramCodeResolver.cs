// -----------------------------------------------------------------------
// <copyright file="ProgramCodeResolver.cs" company="Secretariat of the Pacific Community">
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
    public class ProgramCodeResolver : ValueResolver<string, Tubs.ObserverProgram?>
    {
        protected override Tubs.ObserverProgram? ResolveCore(string source)
        {
            return (Tubs.ObserverProgram?)
                BaseStringResolver.NullableEnumFromString(typeof(Tubs.ObserverProgram), source);
        }
    }
}

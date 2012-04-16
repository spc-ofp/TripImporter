// -----------------------------------------------------------------------
// <copyright file="JobResolver.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Resolvers
{
    using System;
    using AutoMapper;
    using Tubs = Spc.Ofp.Tubs.DAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobResolver : ValueResolver<string, Tubs.Common.JobType?>
    {
        protected override Tubs.Common.JobType? ResolveCore(string source)
        {
            if (String.IsNullOrEmpty(source) || 0 == source.Trim().Length)
                return null;

            /*
             * c:\temp\Obsv_Entry\dbf>c:\Perl\site\bin\dbfdump PS1_CrewType.dbf
                C:Captain
                N:Navigator / Master
                M:Mate
                E:Chief Engineer
                A:Assisstant Engineer
                D:Deck Boss
                O:Cook
                P:Helicopter Pilot
                H:Helicopter Mechanic
                S:Skiff Man
                W:Winch Man
                R:Crew
             */
            switch (source[0])
            {
                case 'C':
                    return Tubs.Common.JobType.Captain;
                case 'N':
                    return Tubs.Common.JobType.NavigatorOrMaster;
                case 'M':
                    return Tubs.Common.JobType.Mate;
                case 'E':
                    return Tubs.Common.JobType.ChiefEngineer;
                case 'A':
                    return Tubs.Common.JobType.AssistantEngineer;
                case 'D':
                    return Tubs.Common.JobType.DeckBoss;
                case 'O':
                    return Tubs.Common.JobType.Cook;
                case 'P':
                    return Tubs.Common.JobType.HelicopterPilot;
                case 'H':
                    return Tubs.Common.JobType.HelicopterMechanic;
                case 'S':
                    return Tubs.Common.JobType.SkiffMan;
                case 'W':
                    return Tubs.Common.JobType.WinchMan;
                case 'R':
                    return Tubs.Common.JobType.Crew;
                default:
                    return null;
            };

        }
    }
}

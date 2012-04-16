// -----------------------------------------------------------------------
// <copyright file="VesselTypeConverter.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Converters
{
    using AutoMapper;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class VesselTypeConverter : ITypeConverter<Observer.Vessel, Tubs.Entities.Vessel>
    {

        public Tubs.Entities.Vessel Convert(ResolutionContext context)
        {
            var sourceVessel = context.SourceValue as Observer.Vessel;
            if (null == sourceVessel)
                return null;

            Tubs.Entities.Vessel vessel = null;
            using (var repo = new Tubs.TubsRepository<Tubs.Entities.Vessel>(Tubs.TubsDataService.GetSession()))
            {
                vessel = repo.FindBy(sourceVessel.Id);
            }
            return vessel;
        }
    }
}

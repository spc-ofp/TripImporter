// -----------------------------------------------------------------------
// <copyright file="MarineDeviceTypeConverter.cs" company="Secretariat of the Pacific Community">
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
    public class MarineDeviceTypeConverter : ITypeConverter<Observer.MarineDevice, Tubs.Entities.MarineDevice>
    {
        public Tubs.Entities.MarineDevice Convert(ResolutionContext context)
        {
            var source = context.SourceValue as Observer.MarineDevice;
            if (null == source)
                return null;

            Tubs.Entities.MarineDevice device = null;
            using (var repo = new Tubs.TubsRepository<Tubs.Entities.MarineDevice>(Tubs.TubsDataService.GetSession()))
            {
                device = repo.FindBy(source.Id);
            }
            return device;
        }
    }
}

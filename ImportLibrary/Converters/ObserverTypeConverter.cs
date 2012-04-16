// -----------------------------------------------------------------------
// <copyright file="ObserverTypeConverter.cs" company="Secretariat of the Pacific Community">
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
    public class ObserverTypeConverter : ITypeConverter<Observer.FieldStaff, Tubs.Entities.Observer>
    {

        public Tubs.Entities.Observer Convert(ResolutionContext context)
        {
            var source = context.SourceValue as Observer.FieldStaff;
            if (null == source)
                return null;

            Tubs.Entities.Observer observer = null;
            using (var repo = new Tubs.TubsRepository<Tubs.Entities.Observer>(Tubs.TubsDataService.GetSession()))
            {
                observer = repo.FindBy(source.StaffCode);
            }
            return observer;
        }
    }
}

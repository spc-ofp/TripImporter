// -----------------------------------------------------------------------
// <copyright file="PortTypeConverter.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary.Converters
{
    using System.Linq;
    using AutoMapper;
    using Observer = Spc.Ofp.Legacy.Observer.Entities;
    using Tubs = Spc.Ofp.Tubs.DAL;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PortTypeConverter : ITypeConverter<Observer.Port, Tubs.Entities.Port>
    {
        public const string DefaultPortName = "UNKNOWN PORT";
        public const string DefaultPortCode = "XYZZY";
        
        public Tubs.Entities.Port Convert(ResolutionContext context)
        {
            var sourcePort = context.SourceValue as Observer.Port;
            // Default to an unknown port
            var portName = null == sourcePort ? DefaultPortName : sourcePort.Name.ToUpper().Trim();

            Tubs.Entities.Port port = null;
            using (var repo = new Tubs.TubsRepository<Tubs.Entities.Port>(Tubs.TubsDataService.GetSession()))
            {
                port = repo.FilterBy(p => p.Name == portName).FirstOrDefault();
                if (null == port)
                {
                    port = repo.FindBy(DefaultPortCode);
                }
            }
            return port;
        }
    }
}

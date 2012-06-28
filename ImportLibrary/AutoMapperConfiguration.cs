// -----------------------------------------------------------------------
// <copyright file="AutoMapperConfiguration.cs" company="Secretariat of the Pacific Community">
// Copyright (C) 2012 Secretariat of the Pacific Community
// </copyright>
// -----------------------------------------------------------------------

namespace ImportLibrary
{
    using AutoMapper;
    using ImportLibrary.Converters;
    using ImportLibrary.Profiles;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile<ActivityProfile>();
                    cfg.AddProfile<CrewProfile>();
                    cfg.AddProfile<ElectronicDeviceProfile>();
                    cfg.AddProfile<GearProfile>();
                    cfg.AddProfile<LengthHeaderProfile>();
                    cfg.AddProfile<LengthSampleProfile>();
                    cfg.AddProfile<PollutionDetailProfile>();
                    cfg.AddProfile<PollutionEventProfile>();
                    cfg.AddProfile<SafetyInspectionProfile>();
                    cfg.AddProfile<SeaDayProfile>();
                    cfg.AddProfile<SetCatchProfile>();
                    cfg.AddProfile<SetHaulProfile>();
                    cfg.AddProfile<ConversionFactorProfile>();
                    cfg.AddProfile<SetProfile>();
                    cfg.AddProfile<SightingProfile>();
                    cfg.AddProfile<SpecialSpeciesInteractionProfile>();
                    cfg.AddProfile<TransferProfile>();                   
                    cfg.AddProfile<TripMonitorProfile>();
                    cfg.AddProfile<TripProfile>();
                    cfg.AddProfile<VesselAttributesProfile>();
                    cfg.AddProfile<VesselNotesProfile>();
                    cfg.AddProfile<WellContentProfile>();
                    cfg.AddProfile<WellReconciliationProfile>();
                    // Trim all strings.  If the trimmed string is empty, return a null
                    Mapper.CreateMap<string, string>().ConvertUsing(s =>
                    {
                        var str = s.NullSafeTrim();
                        return "".Equals(str) ? null : str;
                    });
                    // Longline gear has two attributes that switched from int in FoxPro to decimal in TUBS
                    Mapper.CreateMap<int?, decimal?>().ConvertUsing<NullableIntToNullableDecimal>();
                }
            );

        }
    }
}

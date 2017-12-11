﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Models
{
    public class VinDecode
    {
        public int Count { get; set; }

        public string Message { get; set; }

        public string SearchCriteria { get; set; }

        public IEnumerable<VinDecodeResult> Results { get; set; } = new List<VinDecodeResult>();
    }

    public class VinDecodeResult
    {
        public string ABS { get; set; }
        public string AEB { get; set; }
        public string AdaptiveCruiseControl { get; set; }
        public string AdaptiveHeadlights { get; set; }
        public string AdditionalErrorText { get; set; }
        public string AirBagLocCurtain { get; set; }
        public string AirBagLocFront { get; set; }
        public string AirBagLocKnee { get; set; }
        public string AirBagLocSeatCushion { get; set; }
        public string AirBagLocSide { get; set; }
        public string Artemis { get; set; }
        public string AxleConfiguration { get; set; }
        public string Axles { get; set; }
        public string BasePrice { get; set; }
        public string BatteryA { get; set; }
        public string BatteryA_to { get; set; }
        public string BatteryCells { get; set; }
        public string BatteryInfo { get; set; }
        public string BatteryKWh { get; set; }
        public string BatteryKWh_to { get; set; }
        public string BatteryModules { get; set; }
        public string BatteryPacks { get; set; }
        public string BatteryType { get; set; }
        public string BatteryV { get; set; }
        public string BatteryV_to { get; set; }
        public string BedLengthIN { get; set; }
        public string BedType { get; set; }
        public string BlindSpotMon { get; set; }
        public string BodyCabType { get; set; }
        public string BodyClass { get; set; }
        public string BrakeSystemDesc { get; set; }
        public string BrakeSystemType { get; set; }
        public string BusFloorConfigType { get; set; }
        public string BusLength { get; set; }
        public string BusType { get; set; }
        public string CAFEBodyType { get; set; }
        public string CAFEMake { get; set; }
        public string CAFEModel { get; set; }
        public string CashForClunkers { get; set; }
        public string ChargerLevel { get; set; }
        public string ChargerPowerKW { get; set; }
        public string CoolingType { get; set; }
        public string Country { get; set; }
        public string CurbWeightLB { get; set; }
        public string CustomMotorcycleType { get; set; }
        public string DestinationMarket { get; set; }
        public string DisplacementCC { get; set; }
        public string DisplacementCI { get; set; }
        public string DisplacementL { get; set; }
        public string Doors { get; set; }
        public string DriveType { get; set; }
        public string DriverAssist { get; set; }
        public string ESC { get; set; }
        public string EVDriveUnit { get; set; }
        public string ElectrificationLevel { get; set; }
        public string EngineConfiguration { get; set; }
        public string EngineCycles { get; set; }
        public string EngineCylinders { get; set; }
        public string EngineHP { get; set; }
        public string EngineHP_to { get; set; }
        public string EngineKW { get; set; }
        public string EngineManufacturer { get; set; }
        public string EngineModel { get; set; }
        public string EntertainmentSystem { get; set; }
        public string EquipmentType { get; set; }
        public string ErrorCode { get; set; }
        public string ForwardCollisionWarning { get; set; }
        public string FuelInjectionType { get; set; }
        public string FuelTypePrimary { get; set; }
        public string FuelTypeSecondary { get; set; }
        public string GVWR { get; set; }
        public string LaneDepartureWarning { get; set; }
        public string LaneKeepSystem { get; set; }
        public string Make { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerId { get; set; }
        public string ManufacturerType { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public string MotorcycleChassisType { get; set; }
        public string MotorcycleSuspensionType { get; set; }
        public string NCAPBodyType { get; set; }
        public string NCAPMake { get; set; }
        public string NCAPModel { get; set; }
        public string NCICCode { get; set; }
        public string NCSABodyType { get; set; }
        public string NCSAMake { get; set; }
        public string NCSAModel { get; set; }
        public string Note { get; set; }
        public string OtherBusInfo { get; set; }
        public string OtherEngineInfo { get; set; }
        public string OtherMotorcycleInfo { get; set; }
        public string OtherRestraintSystemInfo { get; set; }
        public string OtherTrailerInfo { get; set; }
        public string ParkAssist { get; set; }
        public string PlantCity { get; set; }
        public string PlantCompanyName { get; set; }
        public string PlantCountry { get; set; }
        public string PlantState { get; set; }
        public string PossibleValues { get; set; }
        public string Pretensioner { get; set; }
        public string RearVisibilityCamera { get; set; }
        public string SeatBeltsAll { get; set; }
        public string SeatRows { get; set; }
        public string Seats { get; set; }
        public string Series { get; set; }
        public string Series2 { get; set; }
        public string SteeringLocation { get; set; }
        public string SuggestedVIN { get; set; }
        public string TopSpeedMPH { get; set; }
        public string TrackWidth { get; set; }
        public string TractionControl { get; set; }
        public string TrailerBodyType { get; set; }
        public string TrailerLength { get; set; }
        public string TrailerType { get; set; }
        public string TransmissionSpeeds { get; set; }
        public string TransmissionStyle { get; set; }
        public string Trim { get; set; }
        public string Trim2 { get; set; }
        public string Turbo { get; set; }
        public string VIN { get; set; }
        public string ValveTrainDesign { get; set; }
        public string VehicleType { get; set; }
        public string WheelBaseLong { get; set; }
        public string WheelBaseShort { get; set; }
        public string WheelBaseType { get; set; }
        public string WheelSizeFront { get; set; }
        public string WheelSizeRear { get; set; }
        public string Wheels { get; set; }
        public string Windows { get; set; }
    }
}

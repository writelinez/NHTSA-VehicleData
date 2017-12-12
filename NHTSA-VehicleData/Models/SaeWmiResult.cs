using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Models
{
    public class SaeWmiResult
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public object CancellationDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CreationDate { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerEmail { get; set; }
        public string ManufacturerFax { get; set; }
        public string ManufacturerPhone { get; set; }
        public string ModificationDate { get; set; }
        public string Over1kManufactured { get; set; }
        public string PostalCode { get; set; }
        public string PrimaryContactJobTitle { get; set; }
        public string PrimaryContactName { get; set; }
        public string Request1Description { get; set; }
        public string Request2Description { get; set; }
        public string RequestCode1 { get; set; }
        public string RequestCode2 { get; set; }
        public string SecondaryContactJobTitle { get; set; }
        public string SecondaryContactName { get; set; }
        public string State { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleType { get; set; }
        public string WMICode { get; set; }
        public string WMICodeExtended { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Models
{
    public class ManufacturerDetailsResult
    {
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ContactEmail { get; set; }
        public object ContactFax { get; set; }
        public string ContactPhone { get; set; }
        public string Country { get; set; }
        public object DBAs { get; set; }
        public ICollection<object> EquipmentItems { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<ManufacturerDetailsManufacturerType> ManufacturerTypes { get; set; }
        public string Mfr_CommonName { get; set; }
        public int Mfr_ID { get; set; }
        public string Mfr_Name { get; set; }
        public string OtherManufacturerDetails { get; set; }
        public string PostalCode { get; set; }
        public object PrimaryProduct { get; set; }
        public string PrincipalFirstName { get; set; }
        public object PrincipalLastName { get; set; }
        public string PrincipalPosition { get; set; }
        public string StateProvince { get; set; }
        public string SubmittedName { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public string SubmittedPosition { get; set; }
        public ICollection<ManufacturerDetailsVehicleType> VehicleTypes { get; set; }
    }

    public class ManufacturerDetailsManufacturerType
    {
        public string Name { get; set; }
    }

    public class ManufacturerDetailsVehicleType
    {
        public string GVWRFrom { get; set; }
        public string GVWRTo { get; set; }
        public bool IsPrimary { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Models
{
    public class ManufacturersResult
    {
        public string Country { get; set; }
        public string Mfr_CommonName { get; set; }
        public int Mfr_ID { get; set; }
        public string Mfr_Name { get; set; }
        public ICollection<ManufacturersVehicleType> VehicleTypes { get; set; } = new List<ManufacturersVehicleType>();
    }

    public class ManufacturersVehicleType
    {
        public bool IsPrimary { get; set; }
        public string Name { get; set; }
    }
}

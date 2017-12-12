using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Models
{
    public class ManufacturerWmiResult
    {
        public string Country { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string VehicleType { get; set; }
        public string WMI { get; set; }
    }
}

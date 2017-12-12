using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Models
{
    public class WmiDecodeResult
    {
        public string CommonName { get; set; }
        public string Make { get; set; }
        public string ManufacturerName { get; set; }
        public string ParentCompanyName { get; set; }
        public string URL { get; set; }
        public string VehicleType { get; set; }
    }
}

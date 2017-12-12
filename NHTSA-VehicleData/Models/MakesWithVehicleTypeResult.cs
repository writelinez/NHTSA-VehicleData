using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Models
{
    public class MakesWithVehicleTypeResult
    {
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; }
    }
}

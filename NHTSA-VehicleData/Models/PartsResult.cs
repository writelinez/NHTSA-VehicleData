using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Models
{
    public class PartsResult
    {
        public string CoverLetterURL { get; set; }
        public string LetterDate { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string ModelYearFrom { get; set; }
        public string ModelYearTo { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string URL { get; set; }
    }
}

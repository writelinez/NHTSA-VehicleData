using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Models
{
    public class OdiRecallsResult
    {
        public string Manufacturer { get; set; }
        public string NHTSACampaignNumber { get; set; }
        public DateTime ReportReceivedDate { get; set; }
        public string Component { get; set; }
        public string Summary { get; set; }
        public string Conequence { get; set; }
        public string Remedy { get; set; }
        public string Notes { get; set; }
        public string ModelYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
}

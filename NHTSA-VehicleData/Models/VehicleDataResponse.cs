using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Models
{
    public class VehicleDataResponse<TModel> where TModel : class
    {
        public int Count { get; set; }

        public string Message { get; set; }

        public string SearchCriteria { get; set; }

        public IEnumerable<TModel> Results { get; set; } = new List<TModel>();
    }
}

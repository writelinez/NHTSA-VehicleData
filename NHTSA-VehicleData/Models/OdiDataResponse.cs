using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Models
{
    public class OdiDataResponse<TModel> where TModel : class
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public ICollection<TModel> Results { get; set; } = new List<TModel>();
    }
}

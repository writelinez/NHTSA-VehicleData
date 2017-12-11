using NHTSAVehicleData.Contracts;
using NHTSAVehicleData.Core;
using NHTSAVehicleData.Core.Attributes;
using NHTSAVehicleData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData
{
    public class NHTSAClient: IDisposable
    {
        private readonly IRestApi _vehicleDataRestApi;
        private readonly IRestApi _recallsRestApi;

        public NHTSAClient()
        {
            _vehicleDataRestApi = new RestApi(Constants.NHTSA_VEHICLE_DATA_API_URL);
            _recallsRestApi = new RestApi(Constants.NHTSA_VEHICLE_RECALLS_API_URL);
        }

        [FunctionInfo(name: "DecodeVin", parameters: "VIN", usage: "DecodeVin VIN=KMHCT4AEXGU980888")]
        public VinDecode DecodeVin(string vin)
        {
            return _vehicleDataRestApi.Get<VinDecode>("decodevinvalues", vin);
        }

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~NHTSAClient()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_vehicleDataRestApi != null)
                {
                    _vehicleDataRestApi.Dispose();
                }
                if (_recallsRestApi != null)
                {
                    _recallsRestApi.Dispose();
                }
            }
        }
        #endregion
    }
}

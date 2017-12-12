using NHTSAVehicleData.Contracts;
using NHTSAVehicleData.Core;
using NHTSAVehicleData.Core.Attributes;
using NHTSAVehicleData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// The Decode VIN API will decode the VIN and the decoded output will be made available in the format of Key-value pairs. The IDs (VariableID and ValueID) represent the unique ID associated with the Variable/Value. In case of text variables, the ValueID is not applicable. Model Year in the request allows for the decoding to specifically be done in the current, or older (pre-1980), model year ranges. It is recommended to always send in the model year. This API also supports partial VIN decoding (VINs that are less than 17 characters). In this case, the VIN will be decoded partially with the available characters. In case of partial VINs, a "*" could be used to indicate the unavailable characters. The 9th digit is not necessary.
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<VinDecodeResult>> DecodeVinAsync(string vin)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<VinDecodeResult>>("decodevinvalues", vin);
        }

        /// <summary>
        /// The Decode VIN API will decode the VIN and the decoded output will be made available in the format of Key-value pairs. The IDs (VariableID and ValueID) represent the unique ID associated with the Variable/Value. In case of text variables, the ValueID is not applicable. Model Year in the request allows for the decoding to specifically be done in the current, or older (pre-1980), model year ranges. It is recommended to always send in the model year. This API also supports partial VIN decoding (VINs that are less than 17 characters). In this case, the VIN will be decoded partially with the available characters. In case of partial VINs, a "*" could be used to indicate the unavailable characters. The 9th digit is not necessary.
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        [FunctionInfo(name: "DecodeVin", parameters: "VIN", usage: "DecodeVin VIN=KMHCT4AEXGU980888")]
        public VehicleDataResponse<VinDecodeResult> DecodeVin(string vin)
        {
            return DecodeVinAsync(vin).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This decodes a batch of VINs that are submitted in a standardized format in a string to return multiple decodes in a flat format.
        /// </summary>
        /// <param name="vinNumbers"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<VinDecodeResult>> BatchDecodeVinAsync(params string[] vinNumbers)
        {
            if (vinNumbers != null && vinNumbers.Any())
            {
                ICollection<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();
                formData.Add(new KeyValuePair<string, string>("DATA", string.Join(";", vinNumbers)));

                return await _vehicleDataRestApi.PostAsync<VehicleDataResponse<VinDecodeResult>>(formData, "DecodeVINValuesBatch");
            }

            throw new ArgumentException("Missing vin numbers.");
        }

        /// <summary>
        /// This decodes a batch of VINs that are submitted in a standardized format in a string to return multiple decodes in a flat format.
        /// </summary>
        /// <param name="vinNumbers"></param>
        /// <returns></returns>
        [FunctionInfo(name: "BatchDecodeVin", parameters: "VINNUMBERS", usage: "BatchDecodeVin VINNUMBERS=KMHCT4AEXGU980888")]
        public VehicleDataResponse<VinDecodeResult> BatchDecodeVin(params string[] vinNumbers)
        {
            return BatchDecodeVinAsync(vinNumbers).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This provides information on the World Manufacturer Identifier for a specific WMI code. WMIs may be put in as either 3 characters representing VIN position 1-3 or 6 characters representing VIN positions 1-3 & 12-14. Example "JTD", "1T9131".
        /// </summary>
        /// <param name="wmi"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<WmiDecodeResult>> DecodeWmiAsync(string wmi)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<WmiDecodeResult>>("DecodeWMI", wmi);
        }

        /// <summary>
        /// This provides information on the World Manufacturer Identifier for a specific WMI code. WMIs may be put in as either 3 characters representing VIN position 1-3 or 6 characters representing VIN positions 1-3 & 12-14. Example "JTD", "1T9131".
        /// </summary>
        /// <param name="wmi"></param>
        /// <returns></returns>
        [FunctionInfo(name: "DecodeWmi", parameters: "WMI", usage: "DecodeWmi WMI=1FD")]
        public VehicleDataResponse<WmiDecodeResult> DecodeWmi(string wmi)
        {
            return DecodeWmiAsync(wmi).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This provides information on the World Manufacturer Identifier for a specific WMI code. WMIs may be put in as either 3 characters representing VIN position 1-3 or 6 characters representing VIN positions 1-3 & 12-14. Example "JTD", "1T9131".
        /// </summary>
        /// <param name="vinCharacters"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<SaeWmiResult>> DecodeSaeWmiAsync(string vinCharacters)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<SaeWmiResult>>("DecodeSAEWMI", vinCharacters);
        }

        /// <summary>
        /// This provides information on the World Manufacturer Identifier for a specific WMI code. WMIs may be put in as either 3 characters representing VIN position 1-3 or 6 characters representing VIN positions 1-3 & 12-14. Example "JTD", "1T9131".
        /// </summary>
        /// <param name="vinCharacters"></param>
        /// <returns></returns>
        [FunctionInfo(name: "DecodeSaeWmi", parameters: "VINCHARACTERS", usage: "DecodeSaeWmi VINCHARACTERS=109017")]
        public VehicleDataResponse<SaeWmiResult> DecodeSaeWmi(string vinCharacters)
        {
            return DecodeSaeWmiAsync(vinCharacters).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Provides information on the all World Manufacturer Identifier (WMI) for a specified Manufacturer. Only WMI registered in vPICList are displayed. For a list of all WMIs for a specified Manufacturer see GetSAEWMIsForManufacturer
        /// </summary>
        /// <param name="wmi"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<ManufacturerWmiResult>> GetWmisForManufacturerAsync(string wmi)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<ManufacturerWmiResult>>("GetWMIsForManufacturer", wmi);
        }

        /// <summary>
        /// Provides information on the all World Manufacturer Identifier (WMI) for a specified Manufacturer. Only WMI registered in vPICList are displayed. For a list of all WMIs for a specified Manufacturer see GetSAEWMIsForManufacturer
        /// </summary>
        /// <param name="wmi"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetWmisForManufacturer", parameters: "WMI", usage: "GetWmisForManufacturer WMI=hon")]
        public VehicleDataResponse<ManufacturerWmiResult> GetWmisForManufacturer(string wmi)
        {
            return GetWmisForManufacturerAsync(wmi).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Provides information on the all World Manufacturer Identifier (WMI) for a specified Manufacturer. All WMI registered with SAE are displayed. For a list of WMIs registered with vPICList see GetWMIsForManufacturer
        /// </summary>
        /// <param name="vinCharacters"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<ManufacturerSaeWmiResult>> GetSaeWmisForManufacturerAsync(string vinCharacters)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<ManufacturerSaeWmiResult>>("GetSAEWMIsForManufacturer", vinCharacters);
        }

        /// <summary>
        /// Provides information on the all World Manufacturer Identifier (WMI) for a specified Manufacturer. All WMI registered with SAE are displayed. For a list of WMIs registered with vPICList see GetWMIsForManufacturer
        /// </summary>
        /// <param name="vinCharacters"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetSaeWmisForManufacturer", parameters: "VINCHARACTERS", usage: "GetSaeWmisForManufacturer VINCHARACTERS=hon")]
        public VehicleDataResponse<ManufacturerSaeWmiResult> GetSaeWmisForManufacturer(string vinCharacters)
        {
            return GetSaeWmisForManufacturerAsync(vinCharacters).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This provides a list of all the Makes available in vPIC Dataset.
        /// </summary>
        /// <returns></returns>
        public async Task<VehicleDataResponse<MakesResult>> GetAllMakesAsync()
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<MakesResult>>("GetAllMakes");
        }

        /// <summary>
        /// This provides a list of all the Makes available in vPIC Dataset.
        /// </summary>
        /// <returns></returns>
        [FunctionInfo(name: "GetAllMakes", parameters: "", usage: "GetAllMakes")]
        public VehicleDataResponse<MakesResult> GetAllMakes()
        {
            return GetAllMakesAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// This provides a list of ORGs with letter date in the given range of the dates and with specified Type of ORG. Up to 1000 results will be returned at a time.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<PartsResult>> GetPartsAsync(string type, DateTime fromDate, DateTime toDate, int page = 1)
        {
            if (page < 1)
                page = 1;

            Dictionary<string, string> queryString = new Dictionary<string, string>();
            queryString.Add("type", string.IsNullOrEmpty(type) ? "0" : type);
            queryString.Add("fromDate", fromDate.ToString("M/d/yyyy"));
            queryString.Add("toDate", toDate.ToString("M/d/yyyy"));
            queryString.Add("page", page.ToString());

            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<PartsResult>>(new string[] { "GetParts" }, queryString);
        }

        /// <summary>
        /// This provides a list of ORGs with letter date in the given range of the dates and with specified Type of ORG. Up to 1000 results will be returned at a time.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetParts", parameters: "TYPE,FROMDATE,TODATE,PAGE(optional)", usage: "GetParts TYPE=565 FROMDATE=01/01/2015 TODATE=05/05/2015")]
        public VehicleDataResponse<PartsResult> GetParts(string type, DateTime fromDate, DateTime toDate, int page = 1)
        {
            return GetPartsAsync(type, fromDate, toDate, page).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<ManufacturersResult>> GetAllManufacturersAsync(int page = 1)
        {
            if (page < 1)
                page = 1;

            Dictionary<string, string> queryString = new Dictionary<string, string>();
            queryString.Add("page", page.ToString());

            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<ManufacturersResult>>(new string[] { "GetAllManufacturers" }, queryString);
        }

        /// <summary>
        /// This provides a list of all the Manufacturers available in vPIC Dataset. Results are provided in pages of 100 items, use parameter"page" to specify 1-st (default, 2nd, 3rd, ...Nth ... page.)
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetAllManufacturers", parameters: "PAGE(optional)", usage: "GetAllManufacturers PAGE=2")]
        public VehicleDataResponse<ManufacturersResult> GetAllManufacturers(int page = 1)
        {
            return GetAllManufacturersAsync(page).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This provides the details for a specific manufacturer that is requested. This gives the results of all the manufacturers whose name is LIKE the manufacturer name. It accepts a partial manufacturer name as an input. Multiple results are returned in case of multiple matches.
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<ManufacturerDetailsResult>> GetManufacturerDetailsAsync(string manufacturer)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<ManufacturerDetailsResult>>("GetManufacturerDetails", manufacturer);
        }

        /// <summary>
        /// This provides the details for a specific manufacturer that is requested. This gives the results of all the manufacturers whose name is LIKE the manufacturer name. It accepts a partial manufacturer name as an input. Multiple results are returned in case of multiple matches.
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetManufacturerDetails", parameters: "MANUFACTURER", usage: "GetManufacturerDetails MANUFACTURER=honda")]
        public VehicleDataResponse<ManufacturerDetailsResult> GetManufacturerDetails(string manufacturer)
        {
            return GetManufacturerDetailsAsync(manufacturer).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<ManufacturerMakesResult>> GetMakesForManufacturerByManufacturerNameAsync(string manufacturer)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<ManufacturerMakesResult> GetMakesForManufacturerByManufacturerName(string manufacturer)
        {
            return GetMakesForManufacturerByManufacturerNameAsync(manufacturer).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<ManufacturerDetailedMakesResult>> GetMakesForManufacturerByManufacturerNameAndYearAsync(string manufacturer, int year)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<ManufacturerDetailedMakesResult> GetMakesForManufacturerByManufacturerNameAndYear(string manufacturer, int year)
        {
            return GetMakesForManufacturerByManufacturerNameAndYearAsync(manufacturer, year).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<MakesWithVehicleTypeResult>> GetMakesForVehicleTypeByVehicleTypeNameAsync(string vehType)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<MakesWithVehicleTypeResult> GetMakesForVehicleTypeByVehicleTypeName(string vehType)
        {
            return GetMakesForVehicleTypeByVehicleTypeNameAsync(vehType).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<MakesWithVehicleTypeResult>> GetVehicleTypesForMakeByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<MakesWithVehicleTypeResult> GetVehicleTypesForMakeByName(string name)
        {
            return GetVehicleTypesForMakeByNameAsync(name).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<VehicleTypesResult>> GetVehicleTypesForMakeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<VehicleTypesResult> GetVehicleTypesForMakeById(int id)
        {
            return GetVehicleTypesForMakeByIdAsync(id).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<ModelsResult>> GetModelsForMakeAsync(string make)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<ModelsResult> GetModelsForMake(string make)
        {
            return GetModelsForMakeAsync(make).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<ModelsResult>> GetModelsForMakeIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<ModelsResult> GetModelsForMakeId(int id)
        {
            return GetModelsForMakeIdAsync(id).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<ModelsResult>> GetModelsByMakeAndModelYearAsync(string make, int modelYear)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<ModelsResult> GetModelsByMakeAndModelYear(string make, int modelYear)
        {
            return GetModelsByMakeAndModelYearAsync(make, modelYear).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<MakesWithModelResult>> GetModelsByMakeAndVehicleTypeAsync(string make, string vehicleType)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<MakesWithModelResult> GetModelsByMakeAndVehicleType(string make, string vehicleType)
        {
            return GetModelsByMakeAndVehicleTypeAsync(make, vehicleType).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<MakesWithModelResult>> GetModelsByMakeModelYearAndVehicleTypeAsync(string make, int modelYear, string vehicleType)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<MakesWithModelResult> GetModelsByMakeModelYearAndVehicleType(string make, int modelYear, string vehicleType)
        {
            return GetModelsByMakeModelYearAndVehicleTypeAsync(make, modelYear, vehicleType).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<ModelsResult>> GetModelsByMakeIdAndModelYearAsync(int makeId, int modelYear)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<ModelsResult> GetModelsByMakeIdAndModelYear(int makeId, int modelYear)
        {
            return GetModelsByMakeIdAndModelYearAsync(makeId, modelYear).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<MakesWithModelResult>> GetModelsByMakeIdAndVehicleTypeAsync(int makeId, string vehicleType)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<MakesWithModelResult> GetModelsByMakeIdAndVehicleType(int makeId, string vehicleType)
        {
            return GetModelsByMakeIdAndVehicleTypeAsync(makeId, vehicleType).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<MakesWithModelResult>> GetModelsByMakeIdModelYearAndVehicleTypeAsync(int makeId, int modelYear, string vehicleType)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<MakesWithModelResult> GetModelsByMakeIdModelYearAndVehicleType(int makeId, int modelYear, string vehicleType)
        {
            return GetModelsByMakeIdModelYearAndVehicleTypeAsync(makeId, modelYear, vehicleType).GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<VariableNamesResult>> GetVehicleVariablesListAsync()
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<VariableNamesResult> GetVehicleVariablesList()
        {
            return GetVehicleVariablesListAsync().GetAwaiter().GetResult();
        }

        public async Task<VehicleDataResponse<VariableValuesResult>> GetVehicleVariableValuesListAsync(string variableName)
        {
            throw new NotImplementedException();
        }

        public VehicleDataResponse<VariableValuesResult> GetVehicleVariableValuesList(string variableName)
        {
            return GetVehicleVariableValuesListAsync(variableName).GetAwaiter().GetResult();
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

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

        /// <summary>
        /// This returns all the Makes in the vPIC dataset for a specified manufacturer whose name is LIKE the manufacturer name in vPIC Dataset. Manufacturer name can be a partial name, or a full name for more specificity (e.g., "HONDA", "HONDA OF CANADA MFG., INC.", etc.)
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<ManufacturerMakesResult>> GetMakesForManufacturerByManufacturerNameAsync(string manufacturer)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<ManufacturerMakesResult>>("GetMakeForManufacturer", manufacturer);
        }

        /// <summary>
        /// This returns all the Makes in the vPIC dataset for a specified manufacturer whose name is LIKE the manufacturer name in vPIC Dataset. Manufacturer name can be a partial name, or a full name for more specificity (e.g., "HONDA", "HONDA OF CANADA MFG., INC.", etc.)
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetMakesForManufacturerByManufacturerName", parameters: "MANUFACTURER", usage: "GetMakesForManufacturerByManufacturerName MANUFACTURER=honda")]
        public VehicleDataResponse<ManufacturerMakesResult> GetMakesForManufacturerByManufacturerName(string manufacturer)
        {
            return GetMakesForManufacturerByManufacturerNameAsync(manufacturer).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns all the Makes in the vPIC dataset for a specified manufacturer whose name is LIKE the manufacturer name in vPIC Dataset and whose Year From and Year To range cover the specified year Manufacturer name can be a partial name, or a full name for more specificity (e.g., "HONDA", "HONDA OF CANADA MFG., INC.", etc.)
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<ManufacturerDetailedMakesResult>> GetMakesForManufacturerByManufacturerNameAndYearAsync(string manufacturer, int year)
        {
            Dictionary<string, string> queryString = new Dictionary<string, string>();
            queryString.Add("year", year.ToString());

            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<ManufacturerDetailedMakesResult>>(new string[] { "GetMakesForManufacturerAndYear", manufacturer }, queryString);
        }

        /// <summary>
        /// This returns all the Makes in the vPIC dataset for a specified manufacturer whose name is LIKE the manufacturer name in vPIC Dataset and whose Year From and Year To range cover the specified year Manufacturer name can be a partial name, or a full name for more specificity (e.g., "HONDA", "HONDA OF CANADA MFG., INC.", etc.)
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetMakesForManufacturerByManufacturerNameAndYear", parameters: "MANUFACTURER, YEAR", usage: "GetMakesForManufacturerByManufacturerNameAndYear MANUFACTURER=mer YEAR=2013")]
        public VehicleDataResponse<ManufacturerDetailedMakesResult> GetMakesForManufacturerByManufacturerNameAndYear(string manufacturer, int year)
        {
            return GetMakesForManufacturerByManufacturerNameAndYearAsync(manufacturer, year).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns all the Makes in the vPIC dataset for a specified vehicle type whose name is LIKE the vehicle type name in vPIC Dataset. Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="vehType"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<MakesWithVehicleTypeResult>> GetMakesForVehicleTypeByVehicleTypeNameAsync(string vehType)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<MakesWithVehicleTypeResult>>("GetMakesForVehicleType", vehType);
        }

        /// <summary>
        /// This returns all the Makes in the vPIC dataset for a specified vehicle type whose name is LIKE the vehicle type name in vPIC Dataset. Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="vehType"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetMakesForVehicleTypeByVehicleTypeName", parameters: "VEHTYPE", usage: "GetMakesForVehicleTypeByVehicleTypeName VEHTYPE=car")]
        public VehicleDataResponse<MakesWithVehicleTypeResult> GetMakesForVehicleTypeByVehicleTypeName(string vehType)
        {
            return GetMakesForVehicleTypeByVehicleTypeNameAsync(vehType).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns all the Vehicle Types in the vPIC dataset for a specified Make whose name is LIKE the make name in vPIC Dataset. Make name can be a partial name, or a full name for more specificity (e.g., "Merc", "Mercedes Benz", etc.)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<MakesWithVehicleTypeResult>> GetVehicleTypesForMakeByNameAsync(string name)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<MakesWithVehicleTypeResult>>("GetVehicleTypesForMake", name);
        }

        /// <summary>
        /// This returns all the Vehicle Types in the vPIC dataset for a specified Make whose name is LIKE the make name in vPIC Dataset. Make name can be a partial name, or a full name for more specificity (e.g., "Merc", "Mercedes Benz", etc.)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetVehicleTypesForMakeByName", parameters: "NAME", usage: "GetVehicleTypesForMakeByName NAME=mercedes")]
        public VehicleDataResponse<MakesWithVehicleTypeResult> GetVehicleTypesForMakeByName(string name)
        {
            return GetVehicleTypesForMakeByNameAsync(name).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns all the Vehicle Types in the vPIC dataset for a specified Make whose ID equals the make ID in vPIC Dataset.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<VehicleTypesResult>> GetVehicleTypesForMakeByIdAsync(int id)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<VehicleTypesResult>>("GetVehicleTypesForMakeId", id.ToString());
        }

        /// <summary>
        /// This returns all the Vehicle Types in the vPIC dataset for a specified Make whose ID equals the make ID in vPIC Dataset.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetVehicleTypesForMakeById", parameters: "ID", usage: "GetVehicleTypesForMakeById ID=450")]
        public VehicleDataResponse<VehicleTypesResult> GetVehicleTypesForMakeById(int id)
        {
            return GetVehicleTypesForMakeByIdAsync(id).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified Make whose name is LIKE the Make in vPIC Dataset. Make can be a partial, or a full for more specificity (e.g., "Harley", "Harley Davidson", etc.)
        /// </summary>
        /// <param name="make"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<ModelsResult>> GetModelsForMakeAsync(string make)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<ModelsResult>>("GetModelsForMake", make);
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified Make whose name is LIKE the Make in vPIC Dataset. Make can be a partial, or a full for more specificity (e.g., "Harley", "Harley Davidson", etc.)
        /// </summary>
        /// <param name="make"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetModelsForMake", parameters: "MAKE", usage: "GetModelsForMake MAKE=honda")]
        public VehicleDataResponse<ModelsResult> GetModelsForMake(string make)
        {
            return GetModelsForMakeAsync(make).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified Make whose Id is EQUAL the MakeId in vPIC Dataset.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<ModelsResult>> GetModelsForMakeIdAsync(int id)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<ModelsResult>>("GetModelsForMakeId", id.ToString());
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified Make whose Id is EQUAL the MakeId in vPIC Dataset.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetModelsForMakeId", parameters: "ID", usage: "GetModelsForMakeId ID=440")]
        public VehicleDataResponse<ModelsResult> GetModelsForMakeId(int id)
        {
            return GetModelsForMakeIdAsync(id).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose name is LIKE the Make in vPIC Dataset. Make can be a partial, or a full for more specificity (e.g., "Harley", "Harley Davidson", etc.)ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="make"></param>
        /// <param name="modelYear"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<ModelsResult>> GetModelsByMakeAndModelYearAsync(string make, int modelYear)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<ModelsResult>>("GetModelsForMakeYear", "make", make, "modelyear", modelYear.ToString());
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose name is LIKE the Make in vPIC Dataset. Make can be a partial, or a full for more specificity (e.g., "Harley", "Harley Davidson", etc.)ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="make"></param>
        /// <param name="modelYear"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetModelsByMakeAndModelYear", parameters: "MAKE,MODELYEAR", usage: "GetModelsByMakeAndModelYear MAKE=honda MODELYEAR=2015")]
        public VehicleDataResponse<ModelsResult> GetModelsByMakeAndModelYear(string make, int modelYear)
        {
            return GetModelsByMakeAndModelYearAsync(make, modelYear).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose name is LIKE the Make in vPIC Dataset. Make can be a partial, or a full for more specificity (e.g., "Harley", "Harley Davidson", etc.)ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="make"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<MakesWithModelResult>> GetModelsByMakeAndVehicleTypeAsync(string make, string vehicleType)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<MakesWithModelResult>>("GetModelsForMakeYear", "make", make, "vehicletype", vehicleType);
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose name is LIKE the Make in vPIC Dataset. Make can be a partial, or a full for more specificity (e.g., "Harley", "Harley Davidson", etc.)ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="make"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetModelsByMakeAndVehicleType", parameters: "MAKE,VEHICLETYPE", usage: "GetModelsByMakeAndVehicleType MAKE=honda VEHICLETYPE=truck")]
        public VehicleDataResponse<MakesWithModelResult> GetModelsByMakeAndVehicleType(string make, string vehicleType)
        {
            return GetModelsByMakeAndVehicleTypeAsync(make, vehicleType).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose name is LIKE the Make in vPIC Dataset. Make can be a partial, or a full for more specificity (e.g., "Harley", "Harley Davidson", etc.)ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="make"></param>
        /// <param name="modelYear"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<MakesWithModelResult>> GetModelsByMakeModelYearAndVehicleTypeAsync(string make, int modelYear, string vehicleType)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<MakesWithModelResult>>("GetModelsForMakeYear", "make", make, "modelyear", modelYear.ToString(), "vehicletype", vehicleType);
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose name is LIKE the Make in vPIC Dataset. Make can be a partial, or a full for more specificity (e.g., "Harley", "Harley Davidson", etc.)ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="make"></param>
        /// <param name="modelYear"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetModelsByMakeModelYearAndVehicleType", parameters: "MAKE,MODELYEAR,VEHICLETYPE", usage: "GetModelsByMakeModelYearAndVehicleType MAKE=honda MODELYEAR=2015 VEHICLETYPE=truck")]
        public VehicleDataResponse<MakesWithModelResult> GetModelsByMakeModelYearAndVehicleType(string make, int modelYear, string vehicleType)
        {
            return GetModelsByMakeModelYearAndVehicleTypeAsync(make, modelYear, vehicleType).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose Id is EQUAL the MakeId in vPIC Dataset. MakeId is integer ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="makeId"></param>
        /// <param name="modelYear"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<ModelsResult>> GetModelsByMakeIdAndModelYearAsync(int makeId, int modelYear)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<ModelsResult>>("GetModelsForMakeIdYear", "makeid", makeId.ToString(), "modelyear", modelYear.ToString());
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose Id is EQUAL the MakeId in vPIC Dataset. MakeId is integer ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="makeId"></param>
        /// <param name="modelYear"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetModelsByMakeIdAndModelYear", parameters: "MAKEID,MODELYEAR", usage: "GetModelsByMakeIdAndModelYear MAKEID=474 MODELYEAR=2015")]
        public VehicleDataResponse<ModelsResult> GetModelsByMakeIdAndModelYear(int makeId, int modelYear)
        {
            return GetModelsByMakeIdAndModelYearAsync(makeId, modelYear).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose Id is EQUAL the MakeId in vPIC Dataset. MakeId is integer ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="makeId"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<MakesWithModelResult>> GetModelsByMakeIdAndVehicleTypeAsync(int makeId, string vehicleType)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<MakesWithModelResult>>("GetModelsForMakeIdYear", "makeid", makeId.ToString(), "vehicletype", vehicleType);
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose Id is EQUAL the MakeId in vPIC Dataset. MakeId is integer ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="makeId"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetModelsByMakeIdAndVehicleType", parameters: "MAKEID,VEHICLETYPE", usage: "GetModelsByMakeIdAndVehicleType MAKEID=474 VEHICLETYPE=truck")]
        public VehicleDataResponse<MakesWithModelResult> GetModelsByMakeIdAndVehicleType(int makeId, string vehicleType)
        {
            return GetModelsByMakeIdAndVehicleTypeAsync(makeId, vehicleType).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose Id is EQUAL the MakeId in vPIC Dataset. MakeId is integer ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="makeId"></param>
        /// <param name="modelYear"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<MakesWithModelResult>> GetModelsByMakeIdModelYearAndVehicleTypeAsync(int makeId, int modelYear, string vehicleType)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<MakesWithModelResult>>("GetModelsForMakeIdYear", "makeid", makeId.ToString(), "modelyear", modelYear.ToString(), "vehicletype", vehicleType);
        }

        /// <summary>
        /// This returns the Models in the vPIC dataset for a specified year and Make whose Id is EQUAL the MakeId in vPIC Dataset. MakeId is integer ModelYear is integer(greater than 1995) Vehicle Type name can be a partial name, or a full name for more specificity(e.g., "Vehicle", "Moto", "Low Speed Vehicle", etc.)
        /// </summary>
        /// <param name="makeId"></param>
        /// <param name="modelYear"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetModelsByMakeIdModelYearAndVehicleType", parameters: "MAKEID,MODELYEAR,VEHICLETYPE", usage: "GetModelsByMakeIdModelYearAndVehicleType MAKEID=474 MODELYEAR=2015 VEHICLETYPE=truck")]
        public VehicleDataResponse<MakesWithModelResult> GetModelsByMakeIdModelYearAndVehicleType(int makeId, int modelYear, string vehicleType)
        {
            return GetModelsByMakeIdModelYearAndVehicleTypeAsync(makeId, modelYear, vehicleType).GetAwaiter().GetResult();
        }

        /// <summary>
        /// This provides a list of all the Vehicle related variables that are in vPIC dataset. Information on the name, description and the type of the variable is provided.
        /// </summary>
        /// <returns></returns>
        public async Task<VehicleDataResponse<VariableNamesResult>> GetVehicleVariablesListAsync()
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<VariableNamesResult>>("GetVehicleVariableList");
        }

        /// <summary>
        /// This provides a list of all the Vehicle related variables that are in vPIC dataset. Information on the name, description and the type of the variable is provided.
        /// </summary>
        /// <returns></returns>
        [FunctionInfo(name: "GetVehicleVariablesList", parameters: "", usage: "GetVehicleVariablesList")]
        public VehicleDataResponse<VariableNamesResult> GetVehicleVariablesList()
        {
            return GetVehicleVariablesListAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// This provides a list of all the accepted values for a given variable that are stored in vPIC dataset. This applies to only "Look up" type of variables.
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public async Task<VehicleDataResponse<VariableValuesResult>> GetVehicleVariableValuesListAsync(string variableName)
        {
            return await _vehicleDataRestApi.GetAsync<VehicleDataResponse<VariableValuesResult>>("GetVehicleVariableValuesList", variableName);
        }

        /// <summary>
        /// This provides a list of all the accepted values for a given variable that are stored in vPIC dataset. This applies to only "Look up" type of variables.
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        [FunctionInfo(name: "GetVehicleVariableValuesList", parameters: "VARIABLENAME", usage: "GetVehicleVariableValuesList VARIABLENAME=battery")]
        public VehicleDataResponse<VariableValuesResult> GetVehicleVariableValuesList(string variableName)
        {
            return GetVehicleVariableValuesListAsync(variableName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Request a list of available Model Years for a given product type (Vehicle). 
        /// </summary>
        /// <returns></returns>
        public async Task<OdiDataResponse<OdiModelYearsResult>> ODI_GetAllModelYearsAsync()
        {
            return await _recallsRestApi.GetAsync<OdiDataResponse<OdiModelYearsResult>>("vehicle");
        }

        /// <summary>
        /// Request a list of available Model Years for a given product type (Vehicle). 
        /// </summary>
        /// <returns></returns>
        [FunctionInfo(name: "ODI_GetAllModelYears", parameters: "", usage: "ODI_GetAllModelYears")]
        public OdiDataResponse<OdiModelYearsResult> ODI_GetAllModelYears()
        {
            return ODI_GetAllModelYearsAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Request a list of vehicle Makers by providing a specific vehicle ModelYear.
        /// </summary>
        /// <returns></returns>
        public async Task<OdiDataResponse<OdiMakesModelYearResult>> ODI_GetAllMakesForModelYearAsync(int modelYear)
        {
            return await _recallsRestApi.GetAsync<OdiDataResponse<OdiMakesModelYearResult>>("vehicle", "modelyear", modelYear.ToString());
        }

        /// <summary>
        /// Request a list of vehicle Makers by providing a specific vehicle ModelYear.
        /// </summary>
        /// <returns></returns>
        [FunctionInfo(name: "ODI_GetAllMakesForModelYear", parameters: "MODELYEAR", usage: "ODI_GetAllMakesForModelYear MODELYEAR=2000")]
        public OdiDataResponse<OdiMakesModelYearResult> ODI_GetAllMakesForModelYear(int modelYear)
        {
            return ODI_GetAllMakesForModelYearAsync(modelYear).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Request a list of vehicle Models by providing the vehicle Model Year and Maker.
        /// </summary>
        /// <returns></returns>
        public async Task<OdiDataResponse<OdiModelsWithMakeModelYearResult>> ODI_GetAllModelsForMakeAndModelYearAsync(int modelYear, string make)
        {
            return await _recallsRestApi.GetAsync<OdiDataResponse<OdiModelsWithMakeModelYearResult>>("vehicle", "modelyear", modelYear.ToString(), "make", make);
        }

        /// <summary>
        /// Request a list of vehicle Models by providing the vehicle Model Year and Maker.
        /// </summary>
        /// <returns></returns>
        [FunctionInfo(name: "ODI_GetAllModelsForMakeAndModelYear", parameters: "MODELYEAR,MAKE", usage: "ODI_GetAllModelsForMakeAndModelYear MODELYEAR=2000 MAKE=saturn")]
        public OdiDataResponse<OdiModelsWithMakeModelYearResult> ODI_GetAllModelsForMakeAndModelYear(int modelYear, string make)
        {
            return ODI_GetAllModelsForMakeAndModelYearAsync(modelYear, make).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get recalls information for specific vehicle.
        /// </summary>
        /// <param name="modelYear"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<OdiDataResponse<OdiRecallsResult>> ODI_GetRecallsByVehicleAsync(int modelYear, string make, string model)
        {
            return await _recallsRestApi.GetAsync<OdiDataResponse<OdiRecallsResult>>("vehicle", "modelyear", modelYear.ToString(), "make", make, "model", model);
        }

        /// <summary>
        /// Get recalls information for specific vehicle.
        /// </summary>
        /// <param name="modelYear"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [FunctionInfo(name: "ODI_GetRecallsByVehicle", parameters: "MODELYEAR,MAKE,MODEL", usage: "ODI_GetRecallsByVehicle MODELYEAR=2012 MAKE=bmw MODEL=3-series")]
        public OdiDataResponse<OdiRecallsResult> ODI_GetRecallsByVehicle(int modelYear, string make, string model)
        {
            return ODI_GetRecallsByVehicleAsync(modelYear, make, model).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Request a list of recalls by providing the NHTSA Recall Campaign Number.
        /// </summary>
        /// <param name="cn">Campaign Number</param>
        /// <returns></returns>
        public async Task<OdiDataResponse<OdiRecallsCampaignResult>> ODI_GetRecallsByCampaignNumberAsync(string cn)
        {
            return await _recallsRestApi.GetAsync<OdiDataResponse<OdiRecallsCampaignResult>>("vehicle", "campaignnumber", cn);
        }

        /// <summary>
        /// Request a list of recalls by providing the NHTSA Recall Campaign Number.
        /// </summary>
        /// <param name="cn">Campaign Number</param>
        /// <returns></returns>
        [FunctionInfo(name: "ODI_GetRecallsByCampaignNumber", parameters: "CN", usage: "ODI_GetRecallsByCampaignNumber CN=12v176000")]
        public OdiDataResponse<OdiRecallsCampaignResult> ODI_GetRecallsByCampaignNumber(string cn)
        {
            return ODI_GetRecallsByCampaignNumberAsync(cn).GetAwaiter().GetResult();
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

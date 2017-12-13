# NHTSA-VehicleData
NHTSA Vehicle Data Api for dotnet core 1.x

NHTSA is the National Highway Safety Administration. This Api only wrapps standard REST calls to their servers. 
More information on the NHTSA's API can be found [here](https://vpic.nhtsa.dot.gov/api/)
and the vehicle recall data API can be found [here](https://one.nhtsa.gov/webapi/Default.aspx?Recalls/API/83).


### Installation
Install the NuGet Package in your project using PM Console or the GUI in Visual Studio

`Install-Package NHTSA-VehicleData -Version 0.1.0`

Register the library with dependecy injection in Startup.cs


### Usage

Important side not here,  You should only use one instance per request as this library uses a single instance of the HttpClient which will consume a single thread for its http calls.
In most basic cases you should register the NHTSA-VehicleData in request scope. In sites with a lot of traffic where you expect to be making 50+ simultaneous requests to the API,
you may want to register it as a singleton.

Some other things to consider, I am not affiliated with the NHTSA in any way, I only made this library for my personal projects and to help anyone out there that wants to save the
time of writing there own libraries to make REST calls the the NHTSA api server. Because this is an open API, I would strongly suggest caching the results and not spamming the NHTSA
servers. The server calls can be slow at times, so that is something else you may want to take in to account.

#### Example of decoding a VIN.
```C#
public class NHTSAClient
{
    private readonly NHTSAClient _nhtsaClient = null;

    public NHTSAClient(NHTSAClient nhtsaClient)
    {
        _nhtsaClient = nhtsaClient;
    }

    public async Task<VinDecodeResult> GiveMeTheVinData(string vinNumber)
    {
        VinDecodeResult result = null;

        VehicleDataResponse<VinDecodeResult> vinResult = await _nhtsaClient.DecodeVinAsync(vinNumber);
        result = vinResult.Results.FirstOrDefault();

        return result;
    }
}
```

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NHTSAVehicleData.Contracts
{
    public interface IRestApi : IDisposable
    {
        Task<TModelOut> GetAsync<TModelOut>(params string[] parameters) where TModelOut : class;
        Task<TModelOut> GetAsync<TModelOut>(string[] urlParameters, Dictionary<string, string> queryParameters) where TModelOut : class;
        Task<TModelOut> PostAsync<TModelOut>(ICollection<KeyValuePair<string, string>> data, params string[] parameters) where TModelOut : class;
    }
}

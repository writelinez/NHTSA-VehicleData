using Newtonsoft.Json;
using NHTSAVehicleData.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Net;

namespace NHTSAVehicleData.Core
{
    internal class RestApi : IRestApi, IDisposable
    {
        private readonly string _baseUrl = string.Empty;
        private readonly HttpClient _httpClient;

        public RestApi(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
        }

        public async Task<TModelOut> GetAsync<TModelOut>(params string[] urlParameters) where TModelOut : class
        {
            return await GetAsync<TModelOut>(urlParameters, null);
        }

        public async Task<TModelOut> GetAsync<TModelOut>(string[] urlParameters, Dictionary<string, string> queryParameters) where TModelOut : class
        {
            UriBuilder uriBuilder = new UriBuilder(_baseUrl);

            if (queryParameters == null)
                queryParameters = new Dictionary<string, string>();

            if (urlParameters != null && urlParameters.Any())
            {
                if (!uriBuilder.Path.EndsWith("/"))
                    uriBuilder.Path += "/";

                foreach (var parameter in urlParameters)
                {
                    uriBuilder.Path += $"{parameter}/";
                }
            }

            if (uriBuilder.Path.EndsWith("/"))
                uriBuilder.Path = uriBuilder.Path.Substring(0, uriBuilder.Path.Length - 1);


            if (!queryParameters.ContainsKey("format"))
                queryParameters.Add("format", "json");

            string sp = string.Join("&", queryParameters.Select(t => $"{t.Key}={WebUtility.UrlEncode(t.Value)}"));

            string reqUrl = uriBuilder.Uri.ToString();
            reqUrl += $"?{sp}";

            HttpResponseMessage responseMessage = await _httpClient.GetAsync(reqUrl);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException(await responseMessage.Content?.ReadAsStringAsync());
            }
            string msg = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TModelOut>(msg);
        }

        public async Task<TModelOut> PostAsync<TModelOut>(ICollection<KeyValuePair<string, string>> data, params string[] parameters) where TModelOut : class
        {
            UriBuilder uriBuilder = new UriBuilder(_baseUrl);
            if (parameters != null)
            {
                if (!uriBuilder.Path.EndsWith("/"))
                    uriBuilder.Path += "/";
                foreach (var parameter in parameters)
                {
                    uriBuilder.Path += $"{parameter}/";
                }
            }

            if (!data.Any(t => t.Key.Equals("format", StringComparison.CurrentCultureIgnoreCase)))
            {
                data.Add(new KeyValuePair<string, string>("format", "JSON"));
            }

            FormUrlEncodedContent content = new FormUrlEncodedContent(data);
            HttpResponseMessage responseMessage = await _httpClient.PostAsync(uriBuilder.Uri.ToString(), content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException(await responseMessage.Content?.ReadAsStringAsync());
            }
            string msg = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TModelOut>(msg);
        }

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~RestApi()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_httpClient != null)
                {
                    _httpClient.Dispose();
                }
            }
        }
        #endregion
    }
}

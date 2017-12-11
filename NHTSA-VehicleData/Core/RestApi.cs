using Newtonsoft.Json;
using NHTSAVehicleData.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

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

        public TModelOut Get<TModelOut>(params string[] parameters) where TModelOut : class
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

            if (uriBuilder.Path.EndsWith("/"))
                uriBuilder.Path = uriBuilder.Path.Substring(0, uriBuilder.Path.Length - 1);

            string reqUrl = uriBuilder.Uri.ToString();
            reqUrl += "?format=json";

            HttpResponseMessage responseMessage = _httpClient.GetAsync(reqUrl).GetAwaiter().GetResult();
            string msg = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

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

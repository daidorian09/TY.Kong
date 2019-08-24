using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TY.Services.Bank.Http
{
    public class RestClient : IRestClient
    {
        #region Members

        private readonly HttpClient _httpClient;
        private readonly ILogger<RestClient> _logger;
        private string _baseUrl { get; set; }
        private int _timeout { get; set; }

        #endregion

        public RestClient(HttpClient httpClient, ILogger<RestClient> logger)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _logger = logger;
        }

        public Task<object> DeleteAsync(string endpoint, object request, Dictionary<string, object> headers = null)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync<T>(string endpoint, object request, Dictionary<string, object> headers = null)
        {
            return null;
        }

        public async Task<object> PostAsync<T>(string endpoint, object request, Dictionary<string, object> headers = null)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_baseUrl);
                _httpClient.Timeout = TimeSpan.FromMilliseconds(_timeout);

                AddHeaders(headers);

                HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(endpoint, request);
                string responseString = await responseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured on PostAsync -> {e.Message}");
                return null;

            }
        }

        public Task<object> PutAsync<T>(string endpoint, object request, Dictionary<string, object> headers = null)
        {
            throw new NotImplementedException();
        }

        public void Configure(string baseUrl, int timeout)
        {
            _baseUrl = baseUrl;
            _timeout = timeout;
        }

        private void AddHeaders(Dictionary<string, object> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
                }
            }
        }
    }
}
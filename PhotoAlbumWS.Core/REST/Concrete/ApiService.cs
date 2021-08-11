using PhotoAlbumWS.Core.REST.Abstract;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbumWS.Core.REST.Concrete
{
    public class ApiService<T> : IApiService<T> where T : class, new()
    {
        private readonly string _baseUrl;
        private readonly IRestClient _restClient;

        public ApiService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _restClient = new RestClient(_baseUrl);
        }

        public async Task<T> Get(string query)
        {
            var request = new RestRequest(query, Method.GET);
            var result = await _restClient.GetAsync<T>(request);

            return result;
        }

        public async Task<List<T>> GetList(string query)
        {
            var request = new RestRequest(query, Method.GET);
            var result = await _restClient.GetAsync<List<T>>(request);

            return result;
        }


        public async Task Delete(string query)
        {
            var request = new RestRequest(query, Method.DELETE);

            await _restClient.DeleteAsync<T>(request);
        }

        public async Task<T> Post(T body)
        {
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(body);

            var response = await _restClient.ExecuteAsync<T>(request);
            return response.Data;
        }

        public async Task<T> Modify(string query, Method method, T body)
        {
            var request = new RestRequest(query, method);
            request.AddJsonBody(body);

            var response = await _restClient.ExecuteAsync<T>(request);
            return response.Data;
        }
    }
}

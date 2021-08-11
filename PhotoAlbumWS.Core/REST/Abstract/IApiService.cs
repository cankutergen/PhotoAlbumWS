using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbumWS.Core.REST.Abstract
{
    public interface IApiService<T> where T : class, new()
    {
        // Queries are added as parameter in case of custom queries

        Task<T> Get(string query);

        Task<List<T>> GetList(string query = "");

        Task<T> Post(T body);

        // Put or Patch
        Task<T> Modify(string query, Method method, T body);

        Task Delete(string query);
    }
}

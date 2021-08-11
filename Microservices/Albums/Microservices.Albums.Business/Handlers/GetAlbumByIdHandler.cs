using MediatR;
using Microservices.Albums.Business.Queries;
using Microservices.Albums.Entities.Concrete;
using PhotoAlbumWS.Core.REST.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Albums.Business.Handlers
{
    public class GetAlbumByIdHandler : IRequestHandler<GetAlbumByIdQuery, Album>
    {

        private readonly IApiService<Album> _apiService;

        public GetAlbumByIdHandler(IApiService<Album> apiService)
        {
            _apiService = apiService;
        }

        // handler for mediator pattern
        public async Task<Album> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
        {
            return await _apiService.Get($"/{request.Id}");
        }
    }
}

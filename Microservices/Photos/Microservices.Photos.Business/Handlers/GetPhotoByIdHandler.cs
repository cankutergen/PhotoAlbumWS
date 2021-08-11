using MediatR;
using Microservices.Photos.Business.Queries;
using Microservices.Photos.Entities.Concrete;
using PhotoAlbumWS.Core.REST.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Photos.Business.Handlers
{
    public class GetPhotoByIdHandler : IRequestHandler<GetPhotoByIdQuery, Photo>
    {
        private readonly IApiService<Photo> _apiService;

        public GetPhotoByIdHandler(IApiService<Photo> apiService)
        {
            _apiService = apiService;
        }

        // handler for mediator pattern
        public async Task<Photo> Handle(GetPhotoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _apiService.Get($"/{request.Id}");
        }
    }
}

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
    public class GetPhotosByAlbumIdHandler : IRequestHandler<GetPhotosByAlbumIdQuery, List<Photo>>
    {
        private readonly IApiService<Photo> _photoApiService;
        private readonly IApiService<AlbumModel> _albumApiService;

        public GetPhotosByAlbumIdHandler(IApiService<Photo> photoApiService, IApiService<AlbumModel> albumApiService)
        {
            _photoApiService = photoApiService;
            _albumApiService = albumApiService;
        }

        // handler for mediator pattern
        public async Task<List<Photo>> Handle(GetPhotosByAlbumIdQuery request, CancellationToken cancellationToken)
        {
            var album = await _albumApiService.Get($"/{request.AlbumId}");
            if(album.Id == 0)
            {
                throw new Exception($"Album with id:{request.AlbumId} does not exist");
            }

            return await _photoApiService.GetList($"?albumId={request.AlbumId}");
        }
    }
}

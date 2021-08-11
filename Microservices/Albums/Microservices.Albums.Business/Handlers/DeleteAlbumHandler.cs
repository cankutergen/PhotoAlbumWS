using MediatR;
using Microservices.Albums.Business.Commands;
using Microservices.Albums.Entities.Concrete;
using PhotoAlbumWS.Core.REST.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Albums.Business.Handlers
{
    public class DeleteAlbumHandler : IRequestHandler<DeleteAlbumCommand, bool>
    {
        private readonly IApiService<Album> _apiService;

        public DeleteAlbumHandler(IApiService<Album> apiService)
        {
            _apiService = apiService;
        }

        // handler for mediator pattern
        public async Task<bool> Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = await _apiService.Get($"/{request.Id}");

            // Album does not exist
            if (album.Id == 0)
            {
                throw new Exception($"Album with id:{request.Id} does not exist");
            }

            await _apiService.Delete($"/{request.Id}");
            return true;
        }
    }
}

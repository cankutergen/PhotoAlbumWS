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
    public class GetAlbumsByUserIdHandler : IRequestHandler<GetAlbumsByUserIdQuery, List<Album>>
    {
        private readonly IApiService<Album> _albumApiService;
        private readonly IApiService<UserModel> _userApiService;

        public GetAlbumsByUserIdHandler(IApiService<Album> albumApiService, IApiService<UserModel> userApiService)
        {
            _albumApiService = albumApiService;
            _userApiService = userApiService;
        }

        // handler for mediator pattern
        public async Task<List<Album>> Handle(GetAlbumsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userApiService.Get($"/{request.UserId}");
            if(user.Id == 0)
            {
                throw new Exception($"User with id:{request.UserId} does not exist");
            }

            return await _albumApiService.GetList($"?userId={request.UserId}");
        }
    }
}

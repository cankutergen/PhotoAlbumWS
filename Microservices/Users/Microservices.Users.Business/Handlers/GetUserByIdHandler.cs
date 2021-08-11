using MediatR;
using Microservices.Users.Business.Queries;
using Microservices.Users.Entities.Concrete;
using PhotoAlbumWS.Core.REST.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Users.Business.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IApiService<User> _apiService;

        public GetUserByIdHandler(IApiService<User> apiService)
        {
            _apiService = apiService;
        }

        // handler for mediator pattern
        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _apiService.Get($"/{request.Id}");
        }
    }
}

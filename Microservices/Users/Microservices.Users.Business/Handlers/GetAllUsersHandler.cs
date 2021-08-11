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
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly IApiService<User> _apiService;

        public GetAllUsersHandler(IApiService<User> apiService)
        {
            _apiService = apiService;
        }

        // handler for mediator pattern
        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _apiService.GetList();
        }
    }
}

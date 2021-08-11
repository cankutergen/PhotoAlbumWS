using MediatR;
using Microservices.Albums.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Albums.Business.Queries
{
    public class GetAlbumsByUserIdQuery : IRequest<List<Album>>
    {
        public int UserId { get; set; }

        // for mocking
        public GetAlbumsByUserIdQuery()
        {

        }

        public GetAlbumsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}

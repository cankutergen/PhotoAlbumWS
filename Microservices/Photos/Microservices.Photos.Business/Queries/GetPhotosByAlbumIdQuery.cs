using MediatR;
using Microservices.Photos.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Photos.Business.Queries
{
    public class GetPhotosByAlbumIdQuery : IRequest<List<Photo>>
    {
        public int AlbumId { get; set; }

        // for mocking
        public GetPhotosByAlbumIdQuery()
        {

        }

        public GetPhotosByAlbumIdQuery(int albumId)
        {
            AlbumId = albumId;
        }
    }
}

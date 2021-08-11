using MediatR;
using Microservices.Albums.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Albums.Business.Queries
{
    public class GetAlbumByIdQuery : IRequest<Album>
    {
        public int Id { get; set; }


        public GetAlbumByIdQuery(int id)
        {
            Id = id;
        }
    }
}

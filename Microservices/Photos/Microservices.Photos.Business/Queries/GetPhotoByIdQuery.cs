using MediatR;
using Microservices.Photos.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Photos.Business.Queries
{
    public class GetPhotoByIdQuery : IRequest<Photo>
    {
        public int Id { get; set; }

        public GetPhotoByIdQuery(int id)
        {
            Id = id;
        }
    }
}

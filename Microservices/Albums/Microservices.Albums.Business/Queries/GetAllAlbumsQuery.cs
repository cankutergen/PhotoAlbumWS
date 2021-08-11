using MediatR;
using Microservices.Albums.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Albums.Business.Queries
{
    public class GetAllAlbumsQuery : IRequest<List<Album>>
    {
    }
}

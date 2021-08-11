using MediatR;
using Microservices.Users.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Users.Business.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}

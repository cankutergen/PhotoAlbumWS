using MediatR;
using Microservices.Users.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Users.Business.Queries
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Users.Entities.Concrete
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public Address Address { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public Company Company { get; set; }
    }
}

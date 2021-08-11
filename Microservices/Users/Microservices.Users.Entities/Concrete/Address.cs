using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Users.Entities.Concrete
{
    public class Address
    {
        public string Street { get; set; }

        public string Suite { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public Geo Geo { get; set; }
    }
}

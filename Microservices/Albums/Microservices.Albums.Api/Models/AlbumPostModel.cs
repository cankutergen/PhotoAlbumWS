﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Albums.Api.Models
{
    public class AlbumPostModel
    {
        public int UserId { get; set; }

        public string Title { get; set; }
    }
}

using MediatR;
using Microservices.Albums.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Albums.Business.Commands
{
    public class CreateAlbumCommand : IRequest<Album>
    {
        public int UserId { get; set; }

        public string Title { get; set; }

        // for mocking
        public CreateAlbumCommand()
        {

        }

        public CreateAlbumCommand(int userId, string title)
        {
            UserId = userId;
            Title = title;
        }
    }
}

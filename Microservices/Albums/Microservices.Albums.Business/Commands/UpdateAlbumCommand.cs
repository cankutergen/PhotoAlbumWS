using MediatR;
using Microservices.Albums.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Albums.Business.Commands
{
    public class UpdateAlbumCommand : IRequest<Album>
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        // for mocking
        public UpdateAlbumCommand()
        {

        }

        public UpdateAlbumCommand(int id, int userId, string title)
        {
            Id = id;
            UserId = userId;
            Title = title;
        }
    }
}

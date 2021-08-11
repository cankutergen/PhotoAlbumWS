using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Albums.Business.Commands
{
    public class DeleteAlbumCommand : IRequest<bool>
    {
        public int Id { get; set; }

        // for mocking
        public DeleteAlbumCommand()
        {

        }

        public DeleteAlbumCommand(int ıd)
        {
            Id = ıd;
        }
    }
}

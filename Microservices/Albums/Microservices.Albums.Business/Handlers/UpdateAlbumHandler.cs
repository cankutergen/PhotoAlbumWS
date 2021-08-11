using FluentValidation;
using MediatR;
using Microservices.Albums.Business.Commands;
using Microservices.Albums.Entities.Concrete;
using PhotoAlbumWS.Core.CrossCuttingConcerns.FluentValidation;
using PhotoAlbumWS.Core.REST.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Albums.Business.Handlers
{
    public class UpdateAlbumHandler : IRequestHandler<UpdateAlbumCommand, Album>
    {
        private readonly IApiService<Album> _apiService;
        private readonly IValidator<Album> _validator;

        public UpdateAlbumHandler(IApiService<Album> apiService, IValidator<Album> validator)
        {
            _apiService = apiService;
            _validator = validator;
        }

        // handler for mediator pattern
        public async Task<Album> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = await _apiService.Get($"/{request.Id}");

            // Album does not exist
            if(album.Id == 0)
            {
                throw new Exception($"Album with id:{request.Id} does not exist");
            }

            album.Title = request.Title;
            album.UserId = request.UserId;

            // Also check validation
            ValidatorTool.FluentValidate(_validator, album);
            return await _apiService.Modify($"/{request.Id}", RestSharp.Method.PUT, album);
        }
    }
}

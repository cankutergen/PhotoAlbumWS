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
    public class CreateAlbumHandler : IRequestHandler<CreateAlbumCommand, Album>
    {
        private readonly IApiService<Album> _apiService;
        private readonly IValidator<Album> _validator;

        public CreateAlbumHandler(IApiService<Album> apiService, IValidator<Album> validator)
        {
            _apiService = apiService;
            _validator = validator;
        }

        // handler for mediator pattern
        public async Task<Album> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = new Album
            {
                Title = request.Title,
                UserId = request.UserId
            };

            // If there are any validation errors, It will throw error
            // Controller will catch the thrown erroe
            ValidatorTool.FluentValidate(_validator, album);
            var response = await _apiService.Post(album);
            return response;
        }
    }
}

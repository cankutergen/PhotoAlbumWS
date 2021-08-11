using FluentValidation;
using Microservices.Albums.Business.Commands;
using Microservices.Albums.Business.Handlers;
using Microservices.Albums.Business.ValidationRules.FluentValidaton;
using Microservices.Albums.Entities.Concrete;
using Moq;
using PhotoAlbumWS.Core.REST.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Microservices.Albums.Business.Tests
{
    public  class UpdateAlbumHandlerTests
    {
        private readonly Mock<IApiService<Album>> _apiService;
        private readonly IValidator<Album> _validator;
        private readonly UpdateAlbumHandler _updateAlbumHandler;

        public UpdateAlbumHandlerTests()
        {
            _apiService = new Mock<IApiService<Album>>();
            _validator = new AlbumValidator();
            _updateAlbumHandler = new UpdateAlbumHandler(_apiService.Object, _validator);
        }

        [Fact]
        public async Task Album_does_not_exist_throws_exception()
        {
            _apiService.Setup(x => x.Get("/1"))
                .Returns(Task.FromResult(new Album { Id = 0, Title = "", UserId = 0 }));

            var command = Mock.Of<UpdateAlbumCommand>(x => x.Id == 1);

            Task result() => _updateAlbumHandler.Handle(command, new CancellationToken());
            await Assert.ThrowsAsync<Exception>(result);
        }

        [Fact]
        public async Task Invalid_model_throws_validation_exception()
        {  
            var entity = new Album
            {
                Id = 1,
                Title = "Title",
                UserId = 1
            };

            _apiService.Setup(x => x.Get("/1"))
                .Returns(Task.FromResult(entity));

            _apiService.Setup(x => x.Modify("/1", RestSharp.Method.PUT, entity))
                .Returns(Task.FromResult(entity));

            var requestModel = Mock.Of<UpdateAlbumCommand>(x => x.Id == 1 && x.Title == "Title");
            Task result() => _updateAlbumHandler.Handle(requestModel, new CancellationToken());

            await Assert.ThrowsAsync<ValidationException>(result);
        }
    }
}

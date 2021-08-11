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
    public class CreateAlbumHandlerTests
    {
        private readonly Mock<IApiService<Album>> _apiService;
        private readonly IValidator<Album> _validator;
        private readonly CreateAlbumHandler _createAlbumHandler;

        public CreateAlbumHandlerTests()
        {
            _apiService = new Mock<IApiService<Album>>();
            _validator = new AlbumValidator();

            _createAlbumHandler = new CreateAlbumHandler(_apiService.Object, _validator);
        }

        [Fact]
        public async Task Invalid_model_throws_validation_exception()
        {
            var requestModel = Mock.Of<CreateAlbumCommand>(x => x.Title == "Title");
            var entity = Mock.Of<Album>(x =>
                x.Title == "Title" &&
                x.UserId == 1
            );

            _apiService.Setup(x => x.Post(entity))
                .Returns(Task.FromResult(entity));

            Task result() => _createAlbumHandler.Handle(requestModel, new CancellationToken());

            await Assert.ThrowsAsync<ValidationException>(result);
        }
    }
}

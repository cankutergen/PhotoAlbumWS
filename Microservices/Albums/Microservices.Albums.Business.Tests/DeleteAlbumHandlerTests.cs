using Microservices.Albums.Business.Commands;
using Microservices.Albums.Business.Handlers;
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
    public class DeleteAlbumHandlerTests
    {
        private readonly Mock<IApiService<Album>> _apiService;
        private readonly DeleteAlbumHandler _deleteAlbumHandler;

        public DeleteAlbumHandlerTests()
        {
            _apiService = new Mock<IApiService<Album>>();
            _deleteAlbumHandler = new DeleteAlbumHandler(_apiService.Object);
        }

        [Fact]
        public async Task Album_does_not_exist_throws_exception()
        {
            _apiService.Setup(x => x.Get("/1"))
                .Returns(Task.FromResult(new Album { Id = 0, Title = "", UserId = 0}));

            var command = Mock.Of<DeleteAlbumCommand>(x => x.Id == 1);

            Task result() => _deleteAlbumHandler.Handle(command, new CancellationToken());
            await Assert.ThrowsAsync<Exception>(result);
        }
    }
}

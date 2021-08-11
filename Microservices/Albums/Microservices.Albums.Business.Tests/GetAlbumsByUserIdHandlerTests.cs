using Microservices.Albums.Business.Handlers;
using Microservices.Albums.Business.Queries;
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
    public class GetAlbumsByUserIdHandlerTests
    {
        private readonly Mock<IApiService<Album>> _albumApiService;
        private readonly Mock<IApiService<UserModel>> _userApiService;
        private readonly GetAlbumsByUserIdHandler _getAlbumsByUserIdHandler;

        public GetAlbumsByUserIdHandlerTests()
        {
            _albumApiService = new Mock<IApiService<Album>>();
            _userApiService = new Mock<IApiService<UserModel>>();
            _getAlbumsByUserIdHandler = new GetAlbumsByUserIdHandler(_albumApiService.Object, _userApiService.Object);
        }

        [Fact]
        public async Task User_does_not_exist_throws_exception()
        {
            _userApiService.Setup(x => x.Get("/1"))
                .Returns(Task.FromResult(new UserModel { Id = 0 }));

            var command = Mock.Of<GetAlbumsByUserIdQuery>(x => x.UserId == 1);

            Task result() => _getAlbumsByUserIdHandler.Handle(command, new CancellationToken());
            await Assert.ThrowsAsync<Exception>(result);
        }
    }
}

using Microservices.Photos.Business.Handlers;
using Microservices.Photos.Business.Queries;
using Microservices.Photos.Entities.Concrete;
using Moq;
using PhotoAlbumWS.Core.REST.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Microservices.Photos.Business.Tests
{
    public class GetPhotosByAlbumIdHandlerTests
    {
        private readonly Mock<IApiService<Photo>> _photoApiService;
        private readonly Mock<IApiService<AlbumModel>> _albumApiService;
        private readonly GetPhotosByAlbumIdHandler _getPhotosByAlbumIdHandler;

        public GetPhotosByAlbumIdHandlerTests()
        {
            _albumApiService = new Mock<IApiService<AlbumModel>>();
            _photoApiService = new Mock<IApiService<Photo>>();
            _getPhotosByAlbumIdHandler = new GetPhotosByAlbumIdHandler(_photoApiService.Object, _albumApiService.Object);
        }

        [Fact]
        public async Task Album_does_not_exist_throws_exception()
        {
            _albumApiService.Setup(x => x.Get("/1"))
                .Returns(Task.FromResult(new AlbumModel { Id = 0 }));

            var command = Mock.Of<GetPhotosByAlbumIdQuery>(x => x.AlbumId == 1);

            Task result() => _getPhotosByAlbumIdHandler.Handle(command, new CancellationToken());
            await Assert.ThrowsAsync<Exception>(result);
        }
    }
}

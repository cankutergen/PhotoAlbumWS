using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microservices.Albums.Api.Models;
using Microservices.Albums.Business.Commands;
using Microservices.Albums.Business.Queries;
using Microservices.Albums.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Microservices.Albums.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memCache;

        public AlbumController(IMediator mediator, IMemoryCache memCache)
        {
            _mediator = mediator;
            _memCache = memCache;
        }

        // GET: api/Album
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cacheKey = "allAlbums";
                List<Album> albums;
                if(!_memCache.TryGetValue(cacheKey, out albums))
                {
                    albums = await _mediator.Send(new GetAllAlbumsQuery());
                }
 
                return new JsonResult(albums);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAlbumsByUserId/{userId}")]
        public async Task<IActionResult> GetAlbumsByUserId(int userId)
        {
            try
            {
                var cacheKey = $"albumsByUserId_{userId}";
                List<Album> albumsByUserId;
                if (!_memCache.TryGetValue(cacheKey, out albumsByUserId))
                {
                    albumsByUserId = await _mediator.Send(new GetAlbumsByUserIdQuery(userId));
                }

                return new JsonResult(albumsByUserId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Album/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var album = await _mediator.Send(new GetAlbumByIdQuery(id));
                return new JsonResult(album);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }          
        }

        // POST: api/Album
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AlbumPostModel albumPostModel)
        {
            try
            {
                var command = new CreateAlbumCommand
                {
                    Title = albumPostModel.Title,
                    UserId = albumPostModel.UserId
                };

                var newAlbum = await _mediator.Send(command);
                return new JsonResult(newAlbum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Album/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AlbumUpdateModel albumUpdateModel)
        {
            try
            {
                var command = new UpdateAlbumCommand
                {
                    Id = id,
                    Title = albumUpdateModel.Title,
                    UserId = albumUpdateModel.UserId
                };

                var updatedAlbum = await _mediator.Send(command);
                return new JsonResult(updatedAlbum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteAlbumCommand(id));
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

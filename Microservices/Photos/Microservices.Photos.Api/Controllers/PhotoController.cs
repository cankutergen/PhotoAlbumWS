using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microservices.Photos.Business.Queries;
using Microservices.Photos.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Microservices.Photos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memCache;

        public PhotoController(IMediator mediator, IMemoryCache memCache)
        {
            _mediator = mediator;
            _memCache = memCache;
        }

        [HttpGet]
        [Route("GetPhotosByAlbumId/{albumId}")]
        public async Task<IActionResult> GetPhotosByAlbumId(int albumId)
        {
            try
            {
                var cacheKey = $"photosByAlbumId_{albumId}";
                List<Photo> photosByAlbumId;
                if (!_memCache.TryGetValue(cacheKey, out photosByAlbumId))
                {
                    photosByAlbumId = await _mediator.Send(new GetPhotosByAlbumIdQuery(albumId));
                }

                return Ok(photosByAlbumId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/Photo/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var photo = await _mediator.Send(new GetPhotoByIdQuery(id));
                return Ok(photo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

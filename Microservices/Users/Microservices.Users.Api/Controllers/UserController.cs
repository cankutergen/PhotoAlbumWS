using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microservices.Users.Business.Queries;
using Microservices.Users.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Microservices.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memCache;

        public UserController(IMediator mediator, IMemoryCache memCache)
        {
            _mediator = mediator;
            _memCache = memCache;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cacheKey = $"allUsers";
                List<User> allUsers;
                if (!_memCache.TryGetValue(cacheKey, out allUsers))
                {
                    allUsers = await _mediator.Send(new GetAllUsersQuery());
                }

                return Ok(allUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery(id));
                return Ok(user);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

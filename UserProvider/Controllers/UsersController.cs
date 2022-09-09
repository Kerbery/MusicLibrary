using Common;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using UserProvider.DTOs;
using UserProvider.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public UsersController(IRepository<User> repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<GetUserDTO>> GetAsync()
        {
            var users = (await _repository.GetAllAsync()).Select(u=>u.AsDTO());

            return users;
        }

        // GET api/<UsersController>/5
        [HttpGet("permalink/{permalink}")]
        public async Task<ActionResult<GetUserDTO>> GetAsync(string permalink)
        {
            var user = await _repository.GetAsync((user)=>user.Permalink==permalink);
            if (user is null)
            {
                return NotFound();
            }

            return user.AsDTO();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> GetAsync(Guid id)
        {
            var user = await _repository.GetAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            return user.AsDTO();
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<GetUserDTO>> PostAsync([FromBody] CreateUserDTO userDTO)
        {
            var user = new User
            {
                UserName = userDTO.UserName,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                AvatarUrl = userDTO.AvatarUrl,
                Permalink = userDTO.Permalink,
                Id = userDTO.Id,
            };

            await _repository.CreateAsync(user);

            await _publishEndpoint.Publish(new UserCreated(user.Id, user.UserName, user.Permalink, user.FirstName, user.FirstName, user.AvatarUrl));

            return CreatedAtAction(nameof(GetAsync), new { id = user.Id }, user.AsDTO());
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateUserDTO userDTO)
        {
            var existingUser = await _repository.GetAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Id = existingUser.Id;
            existingUser.UserName = userDTO.UserName;
            existingUser.FirstName = userDTO.FirstName;
            existingUser.LastName = userDTO.LastName;
            existingUser.AvatarUrl = userDTO.AvatarUrl;
            existingUser.Permalink = userDTO.Permalink;

            await _repository.UpdateAsync(existingUser);
            await _publishEndpoint.Publish(new UserCreated(existingUser.Id, existingUser.UserName, existingUser.Permalink, existingUser.FirstName, existingUser.FirstName, existingUser.AvatarUrl));

            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var existingUser = await _repository.GetAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            await _repository.RemoveAsync(existingUser.Id);
            await _publishEndpoint.Publish(new UserDeleted(existingUser.Id));

            return NoContent();
        }
    }
}

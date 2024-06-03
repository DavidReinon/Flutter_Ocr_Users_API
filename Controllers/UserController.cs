using AzureOcrFlutterAPI.Models;
using AzureOcrFlutterAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AzureOcrFlutterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserRepository userRepository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<User>> GetUserByName(string name)
        {
            var user = await _userRepository.GetUserByName(name);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            var existingUser = await _userRepository.GetUserByName(user.Name!);
            if (existingUser != null)
            {
                return Conflict(new { error = "Este nombre ya existe. Intentalo con otro" });
            }

            await _userRepository.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {

            var existingUser = await _userRepository.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            user.Id = id;
            await _userRepository.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            await _userRepository.DeleteUser(id);
            return NoContent();
        }
    }

}

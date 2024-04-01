﻿using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos.User;
using StoreProject.BLL.Interfaces;

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<UserDto>>> GetUser(string id)
        {
            var user = await _userService.GetUser(id);
            return Ok(user);
        }

        // POST:
        [HttpPost]
        public async Task<ActionResult<UserRegisterDto>> PostUser(UserRegisterDto newUser)
        {
            var createdUserDto = await _userService.AddUser(newUser);
            return Created("", createdUserDto);
        }

        //PUT:
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> PutUser(string id, UserDto user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _userService.UpdateUser(user);
            return NoContent();

        }

        //DELETE:
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {

            await _userService.DeleteUser(id);
            return NoContent();

        }
        // PUT: api/Users/userId/Products/productId
        [HttpPut("{userId}/productId")]
        public async Task<IActionResult> BuyProduct(string userId, string productId)
        {
            await _userService.BuyProduct(userId, productId);
            return NoContent();
        }
    }
}

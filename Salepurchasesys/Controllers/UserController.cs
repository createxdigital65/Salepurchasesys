using Microsoft.AspNetCore.Mvc;
using SalePurchasesys.Models;
using SalePurchasesys.Services;
using SalePurchasesys.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserCreateDto userCreateDto)
        {
            var userEntity = _mapper.Map<User>(userCreateDto);
            var createdUser = await _userService.CreateUserAsync(userEntity);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, _mapper.Map<UserDto>(createdUser));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            if (id != userUpdateDto.Id)
                return BadRequest();

            var userEntity = _mapper.Map<User>(userUpdateDto);
            var updatedUser = await _userService.UpdateUserAsync(id, userEntity);

            if (updatedUser == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] UserLoginDto loginDto)
        {
            var user = await _userService.GetUserByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized();

            return Ok(_mapper.Map<UserDto>(user));
        }
    }
}

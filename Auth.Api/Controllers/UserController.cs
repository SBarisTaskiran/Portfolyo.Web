using System.Collections.Generic;
using App.Data.Entities.Infrastructure;
using App.Data.Entities.Models;
using App.Models.DTO.User;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hasher = BCrypt.Net.BCrypt;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController(IDataRepository dataRepository) : ControllerBase
    {

        [HttpPost("login", Name = "GetUser")]
        public async Task<IActionResult> Get([FromBody] LoginDto login)
        {

            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest();
            }
            string hashedPassword = Hasher.HashPassword(login.Password);
            var user = await dataRepository.GetAll<UserEntity>()
                .Where(u => u.Enabled && u.Email == login.Email)
                .Select(u => new UserGetResult
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    Role = u.Role.Name,
                    Enabled = u.Enabled,
                    PasswordHash = u.PasswordHash
                })
                .SingleOrDefaultAsync();

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Geçersiz email veya şifre.");
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await dataRepository.GetAll<UserEntity>()
                .Select(u => new UserGetResult
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    Role = u.Role.Name,
                    Enabled = u.Enabled,
                })
                .ToListAsync();

            return Ok(users);
        }


        [HttpGet("reset-password-token/{token}")]
        public async Task<IActionResult> GetUserByResetToken(string token)
        {
            var user = await dataRepository.GetAll<UserEntity>()
                .FirstOrDefaultAsync(u => u.ResetPasswordToken == token);

            if (user is null)
            {
                return NotFound();
            }

            user.PasswordHash = string.Empty;
            user.ResetPasswordToken = string.Empty;

            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await dataRepository.GetAll<UserEntity>()
                .Where(u => u.Id == id)
                .Select(u => new UserGetResult
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    Role = u.Role.Name,
                    Enabled = u.Enabled,
                })
                .SingleOrDefaultAsync();

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserEntity user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await dataRepository.UpdateAsync(user);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserEntity user)
        {
            user = await dataRepository.AddAsync(user);
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }
    }
}

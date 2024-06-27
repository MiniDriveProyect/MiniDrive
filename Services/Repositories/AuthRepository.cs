using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniDrive;
using MiniDrive.Data;
using MiniDrive.DTOs;
using MiniDrive.Models;
using MiniDrive.Services.Interfaces;

namespace MiniDrive.Services.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context,IConfiguration configuration)
        {
             _context = context;
            _configuration = configuration;
        }

        public async Task<User> Login(UserDTO user){

            var userLoggin = await _context.Users.FirstOrDefaultAsync(c =>c.Username == user.Username);
            //verificacio con bycript
            if(userLoggin != null){
                if(BCrypt.Net.BCrypt.Verify(user.Password, userLoggin.Password)){

                    return userLoggin;
                }
                return null;
            }
            return null;


        }

        public string generateToken(User user)
        {
           var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
           var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>{
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                 new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer : _configuration["Jwt:Issuer"],
                audience : _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(150),
                signingCredentials: credentials
            );


           return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
    }
}
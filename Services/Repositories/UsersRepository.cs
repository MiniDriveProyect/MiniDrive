using Microsoft.EntityFrameworkCore;
using System.Net;
using MiniDrive.Data;
using MiniDrive.Services.Interfaces;
using MiniDrive.Models;
using MiniDrive.DTOs;
using AutoMapper;

namespace MiniDrive.Services.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UsersRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<(User user, string message, HttpStatusCode statusCode)> Add(UserDTO user)
        {
            var newUser = _mapper.Map<User>(user);
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return (newUser, "User has been successfully created.", HttpStatusCode.Created);
        }

        public async Task<(User user, string message, HttpStatusCode statusCode)> Update(int id, UserDTO user)
        {
            var userUpdate = await _context.Users.FindAsync(id);
            if (userUpdate!= null)
            {
                _mapper.Map(user, userUpdate);
                _context.Entry(userUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return (userUpdate, "The user has been updated correctly.", HttpStatusCode.OK);
            }
            else
                return (default(User)!, $"No user found in the database with Id: {id}.", HttpStatusCode.NotFound);
        }

        public async Task<(IEnumerable<User> users, string message, HttpStatusCode statusCode)> GetAll()
        {
            var users = await _context.Users.Include(u => u.UserFiles).Include(u => u.Folders).ToListAsync();
            if (users.Any())
                return (users, "Users have been successfully obtained.", HttpStatusCode.OK);
            else
                return (Enumerable.Empty<User>(), "No users found in the database.", HttpStatusCode.NotFound);
        }

        public async Task<(User user, string message, HttpStatusCode statusCode)> GetById(int id)
        {
            var user = await _context.Users.Include(u => u.UserFiles).Include(u => u.Folders).FirstOrDefaultAsync(u => u.Id.Equals(id));
            if (user != null)
                return (user, "User has been successfully obtained.", HttpStatusCode.OK);
            else
                return (default(User)!, $"No user found in the database with Id: {id}.", HttpStatusCode.NotFound);
        }

        public async Task<bool> VerifyUser(string dato)
        {
            var user =  await _context.Users.Where(u => u.Username == dato || u.Email == dato).FirstOrDefaultAsync();

            if(user == null)
                return true;
            
            return false;
        }

    }
}
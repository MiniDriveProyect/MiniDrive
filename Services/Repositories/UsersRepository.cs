using Microsoft.EntityFrameworkCore;
using System.Net;
using MiniDrive.Data;
using MiniDrive.Services.Interfaces;
using MiniDrive.Models;
using MiniDrive.DTOs;

namespace MiniDrive.Services.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public Task<(User user, string message, HttpStatusCode statusCode)> Add(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<User> users, string message, HttpStatusCode statusCode)> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<(User user, string message, HttpStatusCode statusCode)> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(User user, string message, HttpStatusCode statusCode)> Update(int id, UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
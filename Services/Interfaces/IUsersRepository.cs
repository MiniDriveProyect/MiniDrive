using System.Net;
using MiniDrive.DTOs;
using MiniDrive.Models;

namespace MiniDrive.Services.Interfaces
{
    public interface IUsersRepository
    {
        Task<(User user, string message, HttpStatusCode statusCode)> Add(UserDTO user);
        Task<(User user, string message, HttpStatusCode statusCode)> Update(int id, UserDTO user);
        Task<(IEnumerable<User> users, string message, HttpStatusCode statusCode)> GetAll();
        Task<(User user, string message, HttpStatusCode statusCode)> GetById(int id);
    }
}
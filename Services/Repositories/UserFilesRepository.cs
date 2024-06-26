using Microsoft.EntityFrameworkCore;
using System.Net;
using MiniDrive.Data;
using MiniDrive.Services.Interfaces;
using MiniDrive.Models;
using MiniDrive.DTOs;

namespace MiniDrive.Services.Repositories
{
    public class UserFileRepository : IUserFileRepository
    {
        public Task<(UserFile userFile, string message, HttpStatusCode statusCode)> ActivateUserFile(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(UserFile userFile, string message, HttpStatusCode statusCode)> Add(UserFileDTO userFile)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<UserFile> userFiles, string message, HttpStatusCode statusCode)> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<UserFile> userFiles, string message, HttpStatusCode statusCode)> GetAllInactive()
        {
            throw new NotImplementedException();
        }

        public Task<(UserFile userFile, string message, HttpStatusCode statusCode)> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(UserFile userFile, string message, HttpStatusCode statusCode)> InactivateUserFile(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(UserFile userFile, string message, HttpStatusCode statusCode)> Update(int id, UserFileDTO userFile)
        {
            throw new NotImplementedException();
        }
    }
}
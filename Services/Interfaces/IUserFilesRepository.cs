using System.Net;
using MiniDrive.DTOs;
using MiniDrive.Models;

namespace MiniDrive.Services.Interfaces
{
    public interface IUserFileRepository
    {
        Task<(UserFile userFile, string message, HttpStatusCode statusCode)> Add(UserFileDTO userFile);
        Task<(UserFile userFile, string message, HttpStatusCode statusCode)> Update(int id, UserFileDTO userFile);
        Task<(UserFile userFile, string message, HttpStatusCode statusCode)> Delete(int id);
        Task<(UserFile userFile, string message, HttpStatusCode statusCode)> Restore(int id);
        Task<(IEnumerable<UserFile> userFiles, string message, HttpStatusCode statusCode)> GetAllDeleted();
        Task<(IEnumerable<UserFile> userFiles, string message, HttpStatusCode statusCode)> GetAll();
        Task<(UserFile userFile, string message, HttpStatusCode statusCode)> GetById(int id);
    }
}
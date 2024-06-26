using System.Net;
using MiniDrive.DTOs;
using MiniDrive.Models;

namespace MiniDrive.Services.Interfaces
{
    public interface IFolderRepository
    {
        Task<(Folder folder, string message, HttpStatusCode statusCode)> Add(FolderDTO folder);
        Task<(Folder folder, string message, HttpStatusCode statusCode)> Update(int id, FolderDTO folder);
        Task<(Folder folder, string message, HttpStatusCode statusCode)> Delete(int id);
        Task<(Folder folder, string message, HttpStatusCode statusCode)> Restore(int id);
        Task<(IEnumerable<Folder> folders, string message, HttpStatusCode statusCode)> GetAllDeleted();
        Task<(IEnumerable<Folder> folders, string message, HttpStatusCode statusCode)> GetAll();
        Task<(Folder folder, string message, HttpStatusCode statusCode)> GetById(int id);
    }
}
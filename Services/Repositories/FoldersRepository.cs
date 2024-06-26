using Microsoft.EntityFrameworkCore;
using System.Net;
using MiniDrive.Data;
using MiniDrive.Services.Interfaces;
using MiniDrive.Models;
using MiniDrive.DTOs;

namespace MiniDrive.Services.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        public Task<(Folder folder, string message, HttpStatusCode statusCode)> ActivateFolder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(Folder folder, string message, HttpStatusCode statusCode)> Add(FolderDTO folder)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<Folder> folders, string message, HttpStatusCode statusCode)> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<Folder> folders, string message, HttpStatusCode statusCode)> GetAllInactive()
        {
            throw new NotImplementedException();
        }

        public Task<(Folder folder, string message, HttpStatusCode statusCode)> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(Folder folder, string message, HttpStatusCode statusCode)> InactivateFolder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(Folder folder, string message, HttpStatusCode statusCode)> Update(int id, FolderDTO folder)
        {
            throw new NotImplementedException();
        }
    }
}
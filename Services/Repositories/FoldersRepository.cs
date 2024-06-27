using Microsoft.EntityFrameworkCore;
using System.Net;
using MiniDrive.Data;
using MiniDrive.Services.Interfaces;
using MiniDrive.Models;
using MiniDrive.DTOs;
using AutoMapper;

namespace MiniDrive.Services.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public FolderRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(Folder folder, string message, HttpStatusCode statusCode)> Add(FolderDTO folder)
        {
            var newFolder = _mapper.Map<Folder>(folder);
            await _context.Folders.AddAsync(newFolder);
            await _context.SaveChangesAsync();
            return (newFolder, "Folder has been successfully created.", HttpStatusCode.Created);
        }

        public async Task<(Folder folder, string message, HttpStatusCode statusCode)> Update(int id, FolderDTO folder)
        {
            var folderUpdate = await _context.Folders.FindAsync(id);
            if (folderUpdate!= null)
            {
                _mapper.Map(folder, folderUpdate);
                _context.Entry(folderUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return (folderUpdate, "The folder has been updated correctly.", HttpStatusCode.OK);
            }
            else
                return (default(Folder)!, $"No folder found in the database with Id: {id}.", HttpStatusCode.NotFound);
        }

        public async Task<(IEnumerable<Folder> folders, string message, HttpStatusCode statusCode)> GetAll(int userId)
        {
            var folders = await _context.Folders.Include(f => f.UserFiles!).Include(f => f.Folders!).Include(f => f.User)
            .Where(f => f.Status!.ToLower() == "active" && f.UserId == userId).ToListAsync();
            if (folders.Any())
                return (folders, "Folders have been successfully obtained.", HttpStatusCode.OK);
            else
                return (Enumerable.Empty<Folder>(), "No folders found in the database.", HttpStatusCode.NotFound);
        }

        public async Task<(IEnumerable<Folder> folders, string message, HttpStatusCode statusCode)> GetAllDeleted(int userId)
        {
            var folders = await _context.Folders.Include(f => f.UserFiles).Include(f => f.User)
            .Where(f => f.Status!.ToLower() == "inactive" && f.UserId == userId).ToListAsync();
            if (folders.Any())
                return (folders, "Deleted folders have been successfully obtained.", HttpStatusCode.OK);
            else
                return (Enumerable.Empty<Folder>(), "No deleted folders found in the database.", HttpStatusCode.NotFound);
        }

        public async Task<(Folder folder, string message, HttpStatusCode statusCode)> GetById(int id, int userId)
        {
            var folder = await _context.Folders.Include(f => f.UserFiles).Include(f => f.User).FirstOrDefaultAsync(f => f.Id.Equals(id) && f.UserId == userId);
            if (folder != null)
                return (folder, "Folder has been successfully obtained.", HttpStatusCode.OK);
            else
                return (default(Folder)!, $"No folder found in the database with Id: {id}.", HttpStatusCode.NotFound);
        }

        public async Task<(Folder folder, string message, HttpStatusCode statusCode)> Delete(int id)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder != null)
            {
                if (folder.Status == "active")
                {
                    return (folder, $"The Folder with Id: {id} is already active.", HttpStatusCode.NotFound);
                }
                else
                {
                    folder.Status = "active";
                    _context.Entry(folder).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return (folder, "The folder has been restored correctly.", HttpStatusCode.OK);
                }
            }
            else
                return (default(Folder)!, $"No folder found in the database with Id: {id}.", HttpStatusCode.NotFound);
        }

        public async Task<(Folder folder, string message, HttpStatusCode statusCode)> Restore(int id)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder != null)
            {
                if (folder.Status == "inactive")
                {
                    return (folder, $"The Folder with Id: {id} is already deleted.", HttpStatusCode.NotFound);
                }
                else
                {
                    folder.Status = "inactive";
                    _context.Entry(folder).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return (folder, "The folder has been deleted correctly.", HttpStatusCode.OK);
                }
            }
            else
                return (default(Folder)!, $"No folder found in the database with Id: {id}.", HttpStatusCode.NotFound);
        }
    }
}
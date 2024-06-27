using Microsoft.EntityFrameworkCore;
using System.Net;
using MiniDrive.Data;
using MiniDrive.Services.Interfaces;
using MiniDrive.Models;
using MiniDrive.DTOs;
using AutoMapper;

namespace MiniDrive.Services.Repositories
{
    public class UserFileRepository : IUserFileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UserFileRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(UserFile userFile, string message, HttpStatusCode statusCode)> Add(UserFileDTO userFile)
        {
            var newUserFile = _mapper.Map<UserFile>(userFile);
            await _context.UserFiles.AddAsync(newUserFile);
            await _context.SaveChangesAsync();
            return (newUserFile, "UserFile has been successfully created.", HttpStatusCode.Created);
        }

        public async Task<(UserFile userFile, string message, HttpStatusCode statusCode)> Update(int id, UserFileDTO userFile)
        {
            var userFileUpdate = await _context.UserFiles.FindAsync(id);
            if (userFileUpdate!= null)
            {
                _mapper.Map(userFile, userFileUpdate);
                _context.Entry(userFileUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return (userFileUpdate, "The userFile has been updated correctly.", HttpStatusCode.OK);
            }
            else
                return (default(UserFile)!, $"No userFile found in the database with Id: {id}.", HttpStatusCode.NotFound);
        }

        public async Task<(IEnumerable<UserFile> userFiles, string message, HttpStatusCode statusCode)> GetAll(int userId)
        {
            var userFiles = await _context.UserFiles.Include(f => f.User)
            .Where(f => f.Status!.ToLower() == "active" && f.UserId == userId).ToListAsync();
            if (userFiles.Any())
                return (userFiles, "UserFiles have been successfully obtained.", HttpStatusCode.OK);
            else
                return (Enumerable.Empty<UserFile>(), "No userFiles found in the database.", HttpStatusCode.NotFound);
        }

        public async Task<(IEnumerable<UserFile> userFiles, string message, HttpStatusCode statusCode)> GetAllDeleted(int userId)
        {
            var userFiles = await _context.UserFiles.Include(f => f.User)
            .Where(f => f.Status!.ToLower() == "inactive" && f.UserId == userId).ToListAsync();
            if (userFiles.Any())
                return (userFiles, "Deleted userFiles have been successfully obtained.", HttpStatusCode.OK);
            else
                return (Enumerable.Empty<UserFile>(), "No deleted userFiles found in the database.", HttpStatusCode.NotFound);
        }

        public async Task<(UserFile userFile, string message, HttpStatusCode statusCode)> GetById(int id, int userId)
        {
            var userFile = await _context.UserFiles.Include(f => f.User).FirstOrDefaultAsync(f => f.Id.Equals(id) && f.UserId == userId);
            if (userFile != null)
                return (userFile, "UserFile has been successfully obtained.", HttpStatusCode.OK);
            else
                return (default(UserFile)!, $"No userFile found in the database with Id: {id}.", HttpStatusCode.NotFound);
        }

        public async Task<(UserFile userFile, string message, HttpStatusCode statusCode)> Delete(int id)
        {
            var userFile = await _context.UserFiles.FindAsync(id);
            if (userFile != null)
            {
                if (userFile.Status == "active")
                {
                    return (userFile, $"The UserFile with Id: {id} is already active.", HttpStatusCode.NotFound);
                }
                else
                {
                    userFile.Status = "active";
                    _context.Entry(userFile).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return (userFile, "The userFile has been restored correctly.", HttpStatusCode.OK);
                }
            }
            else
                return (default(UserFile)!, $"No userFile found in the database with Id: {id}.", HttpStatusCode.NotFound);
        }

        public async Task<(UserFile userFile, string message, HttpStatusCode statusCode)> Restore(int id)
        {
            var userFile = await _context.UserFiles.FindAsync(id);
            if (userFile != null)
            {
                if (userFile.Status == "inactive")
                {
                    return (userFile, $"The UserFile with Id: {id} is already deleted.", HttpStatusCode.NotFound);
                }
                else
                {
                    userFile.Status = "inactive";
                    _context.Entry(userFile).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return (userFile, "The userFile has been deleted correctly.", HttpStatusCode.OK);
                }
            }
            else
                return (default(UserFile)!, $"No userFile found in the database with Id: {id}.", HttpStatusCode.NotFound);
        }
    }
}
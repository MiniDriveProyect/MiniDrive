using Microsoft.EntityFrameworkCore;
using MiniDrive.Models;
using System.ComponentModel.DataAnnotations;

namespace MiniDrive.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
    }
}

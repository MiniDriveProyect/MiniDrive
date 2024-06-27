using Microsoft.EntityFrameworkCore;
using MiniDrive.Models;
using System.ComponentModel.DataAnnotations;

namespace MiniDrive.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relación uno a muchos entre User y Folder
            modelBuilder.Entity<Folder>()
                .HasOne(f => f.User)
                .WithMany(u => u.Folders)
                .HasForeignKey(f => f.UserId);

            // Configurar relación uno a muchos entre Folder y UserFile
            modelBuilder.Entity<UserFile>()
                .HasOne(uf => uf.Folder)
                .WithMany(f => f.UserFiles)
                .HasForeignKey(uf => uf.FolderId);

            // Configurar relación uno a muchos entre User y UserFile
            modelBuilder.Entity<UserFile>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFiles)
                .HasForeignKey(uf => uf.UserId);

            // Configurar relación auto referencial para carpetas anidadas
            modelBuilder.Entity<Folder>()
                .HasMany(f => f.Folders)
                .WithOne()
                .HasForeignKey(f => f.ParentFolderId)
                .OnDelete(DeleteBehavior.Restrict); // para evitar eliminaciones en cascada
        }
    }
}

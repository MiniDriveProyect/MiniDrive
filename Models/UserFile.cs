using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniDrive.Models
{
    public class UserFile
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdateAt { get; set; }
        public int FolderId { get; set; }
        public string Status { get; set; } = "Active";
        public Folder? Folder { get; set; }
        public User? User { get; set; }
    }
}
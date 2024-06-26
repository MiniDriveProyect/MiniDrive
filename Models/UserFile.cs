


namespace MiniDrive.Models
{
    public class UserFile
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public int FolderId { get; set; }
        public string Status { get; set; } = "active";
        public Folder? Folder { get; set; }
        public User? User { get; set; }
    }
}
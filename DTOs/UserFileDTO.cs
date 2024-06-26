

namespace MiniDrive.DTOs
{
    public class UserFileDTO
    {
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public int? FolderId { get; set; }
        public string? Status { get; set; } = "active";
    }
}
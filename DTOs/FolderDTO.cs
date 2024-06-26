

namespace MiniDrive.DTOs
{
    public class FolderDTO
    {
        public string? Name { get; set; }
        public int? UserId { get; set; }
        public int? ParentFolderId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? Status { get; set; } = "active";
    }
}
using System.Text.Json.Serialization;


namespace MiniDrive.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }
        public int ParentFolderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string Status { get; set; } = "active";
        public User? User { get; set; }
        [JsonIgnore]
        public ICollection<Folder>? Folders { get; set; }
        [JsonIgnore]
        public ICollection<UserFile>? UserFiles { get; set; }
    }
}
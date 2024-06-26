using System.Text.Json.Serialization;


namespace MiniDrive.Models
{
        public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        [JsonIgnore]
        public ICollection<Folder>? Folders { get; set; }
        [JsonIgnore]
        public ICollection<UserFile>? UserFiles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        public string Status { get; set; } = "Active";
        public User? User { get; set; }
        [JsonIgnore]
        public ICollection<Folder>? Folders { get; set; }
        [JsonIgnore]
        public ICollection<UserFile>? UserFiles { get; set; }
    }
}
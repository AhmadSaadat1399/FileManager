using System.ComponentModel.DataAnnotations;

namespace FileManager.Models
{
    public class AttachmentFile
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime DateCreation { get; set; }
        public Byte[] MyPathFile { get; set; }
    }
}
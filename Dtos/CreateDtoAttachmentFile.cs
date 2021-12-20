
namespace FileManager.Dtos
{
    public class CreateDtoAttachmentFile
    {
        public string? FileName { get; set; }
        public DateTime DateCreation { get; set; }
        public Byte[]? MyPathFile { get; set; }
    }
}
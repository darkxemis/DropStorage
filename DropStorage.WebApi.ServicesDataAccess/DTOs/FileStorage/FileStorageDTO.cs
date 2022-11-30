namespace DropStorage.WebApi.ServicesDataAccess.DTOs.FileStorage
{
    public class FileStorageDTO
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Name { get; set; } = null!;
        public string Extension { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string SizeMb { get; set; } = null!;
        public Guid UserId { get; set; }

        public virtual UserDTO? User { get; set; }
    }
}



namespace DropStorage.WebApi.ServicesDataAccess.DTOs
{
    public class RolDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<UserDTO> Users { get; set; }
    }
}

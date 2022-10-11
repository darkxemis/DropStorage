using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropStorage.WebApi.ServicesDataAccess.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? ProfilePhotoUrl { get; set; }
        public string DirectoryHome { get; set; } = null!;
        public Guid? RolId { get; set; }

        public virtual RolDTO? Rol { get; set; }
    }
}

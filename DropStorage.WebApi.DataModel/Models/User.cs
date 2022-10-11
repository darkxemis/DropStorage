using DropStorage.WebApi.DataModel.Core.Abstractions;
using System;
using System.Collections.Generic;

namespace DropStorage.WebApi.DataModel.Models
{
    public partial class User : IEntity
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

        public virtual Rol? Rol { get; set; }
    }
}

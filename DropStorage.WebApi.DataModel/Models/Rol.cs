using DropStorage.WebApi.DataModel.Core.Abstractions;
using System;
using System.Collections.Generic;

namespace DropStorage.WebApi.DataModel.Models
{
    public partial class Rol : IEntity
    {
        public Rol()
        {
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}

using DropStorage.WebApi.DataModel.Core.Abstractions;
using System;
using System.Collections.Generic;

namespace DropStorage.WebApi.DataModel.Models
{
    public partial class FileStorage : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Name { get; set; } = null!;
        public string Extension { get; set; } = null!;
        public string Url { get; set; } = null!;
        public long SizeBytes { get; set; }
        public Guid UserId { get; set; }

        public virtual User? User { get; set; }
    }
}

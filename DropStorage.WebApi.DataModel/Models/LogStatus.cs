using DropStorage.WebApi.DataModel.Core.Abstractions;
using System;
using System.Collections.Generic;

namespace DropStorage.WebApi.DataModel.Models
{
    public partial class LogStatus : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string? Endpoint { get; set; }
        public string? Description { get; set; }
        public string? ParameterRecived { get; set; }
        public string? ParameterSended { get; set; }
        public bool? IsError { get; set; }
        public Guid? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}

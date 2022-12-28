using DropStorage.WebApi.DataModel.Core.Abstractions;

namespace DropStorage.WebApi.DataModel.Models
{
    public partial class ShareLinkFileStorage : IEntity
    {
        public int Id { get; set; }
        public Guid IdShareLink { get; set; }
        public Guid IdFileStorage { get; set; }

        public virtual FileStorage IdFileStorageNavigation { get; set; } = null!;
        public virtual ShareLink IdShareLinkNavigation { get; set; } = null!;
    }
}

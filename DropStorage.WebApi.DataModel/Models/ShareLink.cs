using DropStorage.WebApi.DataModel.Core.Abstractions;

namespace DropStorage.WebApi.DataModel.Models
{
    public partial class ShareLink : IEntity
    {
        public ShareLink()
        {
            ShareLinkFileStorages = new HashSet<ShareLinkFileStorage>();
        }

        public Guid Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<ShareLinkFileStorage> ShareLinkFileStorages { get; set; }
    }
}

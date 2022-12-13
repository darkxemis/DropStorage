using DropStorage.WebApi.DataModel.Core.Abstractions;

namespace DropStorage.WebApi.DataModel.Models
{
    public partial class ResetPasswordLink : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}

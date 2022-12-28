namespace DropStorage.WebApi.ServicesDataAccess.DTOs.ShareLink
{
    public class ShareLinkDTO
    {
        public Guid Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }

        public virtual UserDTO User { get; set; } = null!;
    }
}

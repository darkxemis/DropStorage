namespace DropStorage.WebApi.ServicesDataAccess.DTOs.User
{
    public class ResetPasswordDTO
    {
        public Guid RequestPasswordLinkId { get; set; }
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}

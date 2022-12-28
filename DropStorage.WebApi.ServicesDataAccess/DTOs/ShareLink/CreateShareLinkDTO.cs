namespace DropStorage.WebApi.ServicesDataAccess.DTOs.ShareLink
{
    public class CreateShareLinkDTO
    {
        public List<Guid> idsFileStorage { get; set; }
        public List<string> emails { get; set; }
    }
}

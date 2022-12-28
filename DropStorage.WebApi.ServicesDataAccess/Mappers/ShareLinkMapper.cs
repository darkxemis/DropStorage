using AutoMapper;
using DropStorage.WebApi.DataModel.Models;
using DropStorage.WebApi.ServicesDataAccess.DTOs.ShareLink;

namespace DropStorage.WebApi.ServicesDataAccess.Mappers
{
    public class ShareLinkMapper : Profile
    {
        public ShareLinkMapper()
        {
            CreateMap<ShareLink, ShareLinkDTO>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropStorage.WebApi.ServicesDataAccess.DTOs.Auth
{
    public class AuthDTO
    {
        public string access_token { get; set; }

        public string refresh_token { get; set; }
    }
}

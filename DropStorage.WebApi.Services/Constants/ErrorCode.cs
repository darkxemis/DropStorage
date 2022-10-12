using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropStorage.WebApi.Services.Constants
{
    public enum ErrorCode
    {
        // Generic
        UserNotFound = 100,

        // Authentication
        Unauthorized = 101,
        InvalidRefreshToken = 102,

        // New password
        RequestNewPasswordError = 103,
        NewPasswordLinkExpired = 104,
        NewPasswordRequestDisabled = 105,
        NewPasswordMatchError = 106,
        NewPasswordRequestNotFound = 107,
        NewPasswordRequestExpired = 108,

        PasswordNotSecure = 109
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ipstset.Authentication
{
    public interface IUserService
    {
        void CreateIdentityTokenCookie(string token);
        void CreateTraceTokenCookie(string token);
        bool ValidateIdentityToken(string identityToken);
        void SetSessionUserIdentity(UserIdentity userIdentity);
        void ProcessIdentity(string identityToken);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ipstset.Authentication
{
    public interface IUserServiceRepository
    {
        int SaveUserIdentity(UserIdentity userIdentity);
        UserIdentity GetUserIdentity(string identityToken);
        //int SaveAuthorizationToken(AuthorizationToken token, string identityToken);
    }
}

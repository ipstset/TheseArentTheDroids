using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ipstset.Authentication
{
    public class Keys
    {
        public class SessionUserIdentityKeys
        {
            public static readonly string UserIdentityId = "userIdentityId";
            public static readonly string SessionId = "sessionId";
            public static readonly string DomainUserId = "userId";
            public static readonly string IdentityToken = "identityToken";
            public static readonly string TraceToken = "traceToken";
            public static readonly string UserData = "userData";
            public static readonly string DateExpired = "dateExpired";
            public static readonly string DateCreated = "dateCreated";

            public static readonly string UserIdentity = "userIdentity";
        }

    }

}

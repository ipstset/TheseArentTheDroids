using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Ipstset.Authentication
{
    public class SessionUserIdentity
    {
        //public static int UserIdentityId
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[Keys.SessionUserIdentityKeys.UserIdentityId] != null)
        //        {
        //            return Convert.ToInt32(HttpContext.Current.Session[Keys.SessionUserIdentityKeys.UserIdentityId]);
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Keys.SessionUserIdentityKeys.UserIdentityId] = value;
        //    }
        //}
        //public static string IdentityToken
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[Keys.SessionUserIdentityKeys.IdentityToken] != null)
        //        {
        //            return HttpContext.Current.Session[Keys.SessionUserIdentityKeys.IdentityToken].ToString();
        //        }
        //        else
        //        {
        //            return string.Empty;
        //        }
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Keys.SessionUserIdentityKeys.IdentityToken] = value;
        //    }
        //}

        //public static string SessionId
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[Keys.SessionUserIdentityKeys.SessionId] != null)
        //        {
        //            return HttpContext.Current.Session[Keys.SessionUserIdentityKeys.SessionId].ToString();
        //        }
        //        else
        //        {
        //            return string.Empty;
        //        }
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Keys.SessionUserIdentityKeys.SessionId] = value;
        //    }
        //}

        //public static string TraceToken
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[Keys.SessionUserIdentityKeys.TraceToken] != null)
        //        {
        //            return HttpContext.Current.Session[Keys.SessionUserIdentityKeys.TraceToken].ToString();
        //        }
        //        else
        //        {
        //            return string.Empty;
        //        }
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Keys.SessionUserIdentityKeys.TraceToken] = value;
        //    }
        //}

        //public static int DomainUserId
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[Keys.SessionUserIdentityKeys.DomainUserId] != null)
        //        {
        //            return Convert.ToInt32(HttpContext.Current.Session[Keys.SessionUserIdentityKeys.DomainUserId]);
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Keys.SessionUserIdentityKeys.DomainUserId] = value;
        //    }
        //}

        //public static Dictionary<string,string> UserData
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[Keys.SessionUserIdentityKeys.UserData] != null)
        //        {
        //            return (Dictionary<string,string>)HttpContext.Current.Session[Keys.SessionUserIdentityKeys.UserData];
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Keys.SessionUserIdentityKeys.UserData] = value;
        //    }
        //}

        //public static DateTime DateExpired
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[Keys.SessionUserIdentityKeys.DateExpired] != null)
        //        {
        //            return Convert.ToDateTime(HttpContext.Current.Session[Keys.SessionUserIdentityKeys.DateExpired]);
        //        }
        //        else
        //        {
        //            return DateTime.Now;
        //        }
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Keys.SessionUserIdentityKeys.DateExpired] = value;
        //    }
        //}

        //public static DateTime DateCreated
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[Keys.SessionUserIdentityKeys.DateCreated] != null)
        //        {
        //            return Convert.ToDateTime(HttpContext.Current.Session[Keys.SessionUserIdentityKeys.DateCreated]);
        //        }
        //        else
        //        {
        //            return DateTime.Now;
        //        }
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Keys.SessionUserIdentityKeys.DateCreated] = value;
        //    }
        //}

        public static UserIdentity UserIdentity
        {
            get
            {
                if (HttpContext.Current.Session[Keys.SessionUserIdentityKeys.UserIdentity] != null)
                {
                    return (UserIdentity)HttpContext.Current.Session[Keys.SessionUserIdentityKeys.UserIdentity];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session[Keys.SessionUserIdentityKeys.UserIdentity] = value;
            }
        }
    }
}

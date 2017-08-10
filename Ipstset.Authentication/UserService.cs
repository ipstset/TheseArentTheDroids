using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Ipstset.Authentication
{
    public class UserService: IUserService
    {
        private string _identityTokenCookieName = "ips_it";
        private string _traceTokenCookieName = "ips_tt";

        private TimeSpan _traceCookieExpiration;
        private TimeSpan _identityCookieExpiration;

        private IUserServiceRepository _repository;

        public UserService(IUserServiceRepository repository, string idTokenCookieName, string traceTokenCookieName, TimeSpan identityCookieExpiration, TimeSpan traceCookieExpiration)
        {
            _repository = repository;
            _identityTokenCookieName = idTokenCookieName;
            _traceTokenCookieName = traceTokenCookieName;
            _identityCookieExpiration = identityCookieExpiration;
            _traceCookieExpiration = traceCookieExpiration;
        }

        public void CreateIdentityTokenCookie(string token)
        {
            //set cookie
            var cookie = new HttpCookie(_identityTokenCookieName, token);

            var now = DateTime.Now;
            cookie.Expires = now + _identityCookieExpiration;

            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        public void CreateTraceTokenCookie(string token)
        {
            //set cookie
            var cookie = new HttpCookie(_traceTokenCookieName, token);

            var now = DateTime.Now;
            cookie.Expires = now + _traceCookieExpiration;

            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        public bool ValidateIdentityToken(string token)
        {
            UserIdentity userIdentity = SessionUserIdentity.UserIdentity;

            if(SessionUserIdentity.UserIdentity==null)
            {
                //get from repository
                userIdentity = _repository.GetUserIdentity(token);
                SetSessionUserIdentity(userIdentity);
            }

            if (userIdentity.Id == 0)
                return false;
            if (userIdentity.DateExpired <= DateTime.Now)
                return false;

            return true;
        }

        public void SetSessionUserIdentity(UserIdentity userIdentity)
        {
            SessionUserIdentity.UserIdentity = userIdentity;
        }


        #region IUserService Members


        public void ProcessIdentity(string identityToken)
        {
            //get sessionidentity
            var identity = SessionUserIdentity.UserIdentity ?? new UserIdentity();
            if (identity.Id==0 && !String.IsNullOrEmpty(identityToken))
            {
                identity = _repository.GetUserIdentity(identityToken);
                SetSessionUserIdentity(identity);
            }
            
            //set the trace token
            if (String.IsNullOrEmpty(identity.TraceToken))
                identity.TraceToken = Guid.NewGuid().ToString();

            //still unable to get UserIdentity
            if(identity.Id==0)
            {
                identity.IdentityToken = identityToken;
                identity.SessionId = HttpContext.Current.Session.SessionID;
                identity.DomainUserId = 0;
                identity.DateCreated = DateTime.Now;
                identity.DateExpired = DateTime.Now + _identityCookieExpiration;
                identity.UserData = new Dictionary<string, string>();
                identity.UserData.Add("IPAddress", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                identity.UserData.Add("Browser", HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"]);
                identity.UserData.Add("Referrer", HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]);
            }

            //validate the token
            var valid = false;
            if(!string.IsNullOrEmpty(identityToken))
            {
                valid = ValidateIdentityToken(identityToken);
            }

            if (!valid)
            {
                var idToken = Guid.NewGuid().ToString();
                CreateIdentityTokenCookie(idToken);

                identity.IdentityToken = idToken;
                identity.DateCreated = DateTime.Now;
                identity.DateExpired = DateTime.Now + _identityCookieExpiration;
                //save new UserIdentity
                _repository.SaveUserIdentity(identity);

                //update sessionuseridentity
                SetSessionUserIdentity(identity);
            }

            //create trace cookie
            var traceCookie = HttpContext.Current.Request.Cookies[_traceTokenCookieName];
            if(traceCookie==null)
                CreateTraceTokenCookie(identity.TraceToken);

        }

        #endregion
    }
}

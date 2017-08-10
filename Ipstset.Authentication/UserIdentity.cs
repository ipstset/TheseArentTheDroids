using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ipstset.Authentication
{
    public class UserIdentity
    {
        public int Id { get; set; }
        public string IdentityToken { get; set; }
        public string SessionId { get; set; }
        public string TraceToken { get; set; }
        public int DomainUserId { get; set; }
        public Dictionary<string, string> UserData { get; set; }
        public DateTime DateExpired { get; set; }
        public DateTime DateCreated { get; set; }
        //public AuthorizationToken AuthorizationToken { get; set; }

        public string UserDataJson
        {
            get
            {
                var json = string.Empty;
                if (UserData != null && UserData.Count > 0)
                {
                    json = UserData.Aggregate(json, (current, p) => current + String.Format("\"{0}\":\"{1}\",", p.Key, p.Value));
                }

                if (!String.IsNullOrEmpty(json))
                {
                    //remove last comma
                    if (json.EndsWith(","))
                    {
                        json = json.Remove(json.LastIndexOf(","));

                    }

                    //final format of string
                    json = "{" + json + "}";
                }

                return json;
            }
        }
    }
}

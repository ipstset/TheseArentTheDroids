using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ipstset.Authentication
{
    public class AuthorizationToken
    {
        public string Value { get; set; }
        public DateTime DateExpired { get; set; }
        public void Create(int hoursActive = 2)
        {
            Value = Guid.NewGuid().ToString();
            DateExpired = DateTime.Now.AddHours(hoursActive);
        }
        public bool Active { get { return DateExpired > DateTime.Now; } }
    }
}

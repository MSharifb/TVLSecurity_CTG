using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TVLSecurity.Web
{
    public class CustomMemberShip
    {
        public static CustomMembershipProvider Provider
        {
            get { return new CustomMembershipProvider(); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;

namespace TVLSecurity.Web.Models
{
    public class UserGroupFormViewModel : ModelFormViewModelBase
    {
        public UserGroup Group{set;get;}
        public List<Role> RoleList{set;get;}
        public List<UserGroup> GroupList{set;get;}
    }
}

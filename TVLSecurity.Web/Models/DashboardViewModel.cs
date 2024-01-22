
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TVLSecurity.Web.Models
{
    public class DashboardViewModel : ModelFormViewModelBase
    {
        public Dashboard Dashboard { set; get; }
        public IList<Dashboard> DashboardList { set; get; }


        public DashboardViewModel()
        {
            Dashboard = new Dashboard();
            List<Dashboard> DashboardList = new List<Dashboard>();
            IsValidModel = true;
        }

        public void GetZoneWiseUserList()
        {
            this.DashboardList = UserRepository.GetZoneWiseUserList();
        }

    }

}
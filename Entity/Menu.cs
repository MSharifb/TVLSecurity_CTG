using System;
using System.Runtime.Serialization;
using PrimaryBaseLibrary;

namespace Entity
{
    [DataContract]
    public class Menu : EntityBase
    {
        [DataMember, DataColumn(true)]
        public int MenuId { set; get; }

        [DataMember, DataColumn(true)]
        public string MenuName { set; get; }

        [DataMember, DataColumn(true)]
        public string MenuCaption { set; get; }

        [DataMember, DataColumn(true)]
        public string MenuCaptionBng { set; get; }

        [DataMember, DataColumn(true)]
        public int ParentMenuId { set; get; }

        [DataMember, DataColumn(true)]
        public int SerialNo { set; get; }

        [DataMember, DataColumn(true)]
        public string PageUrl { set; get; }

        [DataMember, DataColumn(true)]
        public string ParentMenuName { set; get; }

        [DataMember, DataColumn(false)]
        public Int32 RoleId { set; get; }

        [DataMember, DataColumn(true)]
        public bool IsAssignedMenu { set; get; }

        [DataMember, DataColumn(true)]
        public bool IsAddAssigned { set; get; }

        [DataMember, DataColumn(true)]
        public bool IsEditAssigned { set; get; }

        [DataMember, DataColumn(true)]
        public bool IsDeleteAssigned { set; get; }

        [DataMember, DataColumn(true)]
        public bool IsCancelAssigned { set; get; }

        [DataMember, DataColumn(true)]
        public bool IsPrintAssigned { set; get; }

        private bool viewAssigned;
        [DataMember, DataColumn(false)]
        public bool IsViewAssigned
        {
            get
            {
                if (!IsAddAssigned)
                {
                    if (!IsEditAssigned)
                    {
                        if (!IsDeleteAssigned)
                        {
                            if (!IsCancelAssigned)
                            {
                                if (!IsPrintAssigned)
                                {
                                   viewAssigned=  true;
                                }
                            }
                        }
                    }
                }

                return viewAssigned;
            }
            set
            {
                viewAssigned = value;
            }
        }

        [DataMember, DataColumn(false)]
        public string LoginId { set; get; }

        [DataMember, DataColumn(true)]
        public int ApplicationId { set; get; }

        [DataMember, DataColumn(true)]
        public int ModuleId { set; get; }

        [DataMember, DataColumn(true)]
        public string ApplicationName { set; get; }

        [DataMember, DataColumn(true)]
        public string ModuleName { set; get; }




    }
}

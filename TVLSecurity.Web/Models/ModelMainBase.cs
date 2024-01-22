using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using TVLSecurity.Web.Code;

namespace TVLSecurity.Web.Models
{
    public class ModelMainBase
    {
 
        public int PageNumber { set; get; }
        public int PageSize { set; get; }
        public int TotalRowCount { set; get; }

        public int CurrentPageIndex { set; get; }

        public bool IsValidModel { set; get; }
        public bool NoDataFound { set; get; }
        public string Message { set; get; }
    }
}
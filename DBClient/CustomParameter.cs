﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBClient
{
   public class CustomParameter
   {
       #region Private Members
       private string strParamName;
        private System.Data.DbType dbtType;
        private object objValue;
        private System.Data.ParameterDirection paramDirection;
       #endregion

       #region Public Members
        public String ParamaterName
        {
            set { this.strParamName = value; }
            get { return this.strParamName; }
        }

        public System.Data.DbType ParamType
        {
            set { this.dbtType = value; }
            get { return this.dbtType; }
        }

        public object ParamValue
        {
            set { this.objValue = value; }
            get { return this.objValue; }
        }

        public System.Data.ParameterDirection ParamDirection
        {
            set { this.paramDirection = value; }
            get { return this.paramDirection; }
        }

        public CustomParameter(string strPramName, System.Data.DbType type)
        {
            this.ParamaterName = strPramName;
            this.ParamType = type;            

        }
        public CustomParameter(string strPramName, object objValue, System.Data.DbType type)
        {
            this.ParamaterName = strPramName;
            this.ParamType = type;
            this.ParamValue = objValue;
            this.paramDirection = System.Data.ParameterDirection.Input;

        }

        public CustomParameter(string strPramName, object objValue, System.Data.DbType type, System.Data.ParameterDirection direction)
        {
            this.ParamaterName = strPramName;
            this.ParamType = type;
            this.ParamValue = objValue;
            this.ParamDirection = direction;
        }
        #endregion
   }
       
}

using System;
using System.Collections.Generic;
using System.Text;
using MoleQ.Market.Database;

namespace MQWebApi.Service.Impl
{
    public class BaseService
    {
        public BaseService()
        {
        }

        public BaseService(string connectionString)
        {
            //SybaseHelper.connectionString = connectionString;

            //var auth  = LocalStorage.GetItem("AuthOEM");
            //if (null == auth || !Convert.ToBoolean(auth) )
            //{
            //    SybaseHelper.MoleQOEMSetOption();
            //    LocalStorage.SetItem("AuthOEM", Convert.ToBoolean(true)); 
            //}
        }
    }
}
